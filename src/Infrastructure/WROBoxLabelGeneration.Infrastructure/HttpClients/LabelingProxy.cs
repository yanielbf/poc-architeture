using WROBoxLabelGeneration.Application.Contracts.Infrastructure.HttpClients;
using WROBoxLabelGeneration.Domain.DTOs.HttpClients.Labeling;

namespace WROBoxLabelGeneration.Infrastructure.HttpClients
{
    public class LabelingProxy : BaseProxy, ILabelingProxy
    {
        private readonly HttpClient _httpClient;
        private const string _endpointLabelingShipmentRate = "/api/Labeling/shipmentRate";

        public LabelingProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetShipmentRateResponseDTO> GetShipmentRate(GetShipmentRateRequestDTO getShipmentRateRequest)
        {
            var url = new Uri($"{_endpointLabelingShipmentRate}?" +
                $"referenceTypeId={getShipmentRateRequest.ReferenceTypeId}" +
                $"&referenceId={getShipmentRateRequest.ReferenceId}", UriKind.Relative);
            var response = await _httpClient.GetAsync(url);
            return await ProcessResponseAsync<GetShipmentRateResponseDTO>(
                response,
                _endpointLabelingShipmentRate,
                $"ReferenceType: {getShipmentRateRequest.ReferenceTypeId} - ReferenceId: {getShipmentRateRequest.ReferenceId}"
            );
        }

        protected override string HandleNotFoundErrorMessage(string endpointName, object dataForError)
        {
            switch (endpointName)
            {
                case _endpointLabelingShipmentRate:
                    return $"Could not find shipment rate for parameters {dataForError.ToString()}";
                default:
                    return string.Empty;
            }
        }
    }
}
