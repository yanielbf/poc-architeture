using WROBoxLabelGeneration.Domain.DTOs.HttpClients.Labeling;

namespace WROBoxLabelGeneration.Application.Contracts.Infrastructure.HttpClients
{
    public interface ILabelingProxy
    {
        Task<GetShipmentRateResponseDTO> GetShipmentRate(GetShipmentRateRequestDTO getShipmentRateRequest);
    }
}
