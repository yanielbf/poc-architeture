using WROBoxLabelGeneration.Domain.DTOs.HttpClients.Attachemnts;

namespace WROBoxLabelGeneration.Application.Contracts.Infrastructure.HttpClients
{
    public interface IAttachmentsProxy
    {
        Task<CreatedAttachmentResponseDTO> CreateAttachmentsAsync(CreateAttachmentRequestDTO attachmentsToCreate);
    }
}
