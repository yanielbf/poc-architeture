using LightResults;
using MediatR;

namespace WROBoxLabelGeneration.Application.Features.WroBox.Commands.GenerateLabels
{
    public class GenerateLabelsCommand : IRequest<Result<GenerateLabelsDto>>
    {
        public int WroId { get; set; }

        public bool GetShippingLabels { get; set; }

        public string? BoxLabelCobrand { get; set; }
    }
}
