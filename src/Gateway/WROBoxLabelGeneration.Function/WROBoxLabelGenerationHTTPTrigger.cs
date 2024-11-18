using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using WROBoxLabelGeneration.Application.Features.WroBox.Commands.GenerateLabels;

namespace WROBoxLabelGeneration.Function
{
    public class WROBoxLabelGenerationHTTPTrigger
    {
        protected readonly IMediator _mediator;

        public WROBoxLabelGenerationHTTPTrigger(IMediator mediator, ILoggerFactory loggerFactory)
        {
            _mediator = mediator;
        }

        [Function("WROBoxLabelGeneratorHTTPTrigger")]
        public async Task<IResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            var result = await _mediator.Send(new GenerateLabelsCommand { WroId = 101655, GetShippingLabels = true });
            return Results.Ok();
        }
    }
}
