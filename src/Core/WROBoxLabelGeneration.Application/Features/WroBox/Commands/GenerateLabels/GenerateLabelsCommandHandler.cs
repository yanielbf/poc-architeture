using LightResults;
using LightResults.Extensions.ExceptionHandling;
using MediatR;
using WROBoxLabelGeneration.Application.Contracts.Infrastructure.HttpClients;
using WROBoxLabelGeneration.Application.Contracts.Infrastructure.LabelServices;
using WROBoxLabelGeneration.Application.Contracts.Persistence;
using WROBoxLabelGeneration.Domain.DTOs.HttpClients.Attachemnts;
using WROBoxLabelGeneration.Domain.DTOs.HttpClients.Labeling;
using WROBoxLabelGeneration.SharedKernel.Logger;

namespace WROBoxLabelGeneration.Application.Features.WroBox.Commands.GenerateLabels
{
    public class GenerateLabelsCommandHandler : IRequestHandler<GenerateLabelsCommand, Result<GenerateLabelsDto>>
    {
        private readonly IWroRepository _wroRepository;

        private readonly ILabelingProxy _labelingProxy;

        private readonly IAttachmentsProxy _attachmentsProxy;

        private readonly Func<string, ILabelGenerator> _labelGeneratorFactory;

        public GenerateLabelsCommandHandler(
            IWroRepository wroRepository,
            ILabelingProxy labelingProxy,
            IAttachmentsProxy attachmentsProxy,
            Func<string, ILabelGenerator> labelGeneratorFactory
        )
        {
            _wroRepository = wroRepository;
            _labelingProxy = labelingProxy;
            _attachmentsProxy = attachmentsProxy;
            _labelGeneratorFactory = labelGeneratorFactory;
        }

        public async Task<Result<GenerateLabelsDto>> Handle(GenerateLabelsCommand request, CancellationToken cancellationToken)
        {
            var wro = _wroRepository.GetByIdWithDetails(request.WroId);

            if (wro == null)
            {
                return Result<GenerateLabelsDto>.Fail();
            }

            if (request.GetShippingLabels && wro.HasOriginAddress)
            {
                foreach (var boxPackingDetailId in wro.PackingDetailIdsFromBoxes)
                {
                    var requestLabelingApi = new GetShipmentRateRequestDTO
                    {
                        ReferenceId = boxPackingDetailId,
                        ReferenceTypeId = 2
                    };
                    
                    var response = await _labelingProxy.GetShipmentRate(requestLabelingApi).TryAsync();

                    if (response.IsSuccess && !string.IsNullOrWhiteSpace(response.Value.LabelUrl))
                    {
                        wro.UpdateShippingLabelUrlForBox(boxPackingDetailId, response.Value.LabelUrl);
                    }
                    else
                    {
                        LoggerHelper.LogWarning($"Failed to fetch label for PackageDetailId: {boxPackingDetailId}");

                        if (response.IsFailed)
                        {
                            LoggerHelper.LogError("Failed request to labelingproxy", response.Errors);
                        }
                    }
                }
            }

            var data = await _labelGeneratorFactory("wro-label-generator").CreatePdfAsBytes(wro);

            var requestAttachmentApi = new CreateAttachmentRequestDTO()
            {
                Name = wro.BoxLabelFileName,
                Data = data
            };

            var attachments = await _attachmentsProxy.CreateAttachmentsAsync(requestAttachmentApi);

            wro = _wroRepository.GetById(request.WroId);

            if (wro == null)
            {
                return Result<GenerateLabelsDto>.Fail();
            }

            wro.UpdateBoxLabelURL(attachments.AttachmentUri);
            await _wroRepository.SaveChangesAsync();

            return Result<GenerateLabelsDto>.Ok(new GenerateLabelsDto { 
                AttachmentUri = attachments.AttachmentUri
            });
        }
    }
}
