namespace WROBoxLabelGeneration.Domain.DTOs.HttpClients.Labeling
{
    public class GetShipmentRateResponseDTO
    {
        public int ShipmentRateId { get; set; }
        public int ReferenceId { get; set; }
        public int ReferenceTypeId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string TrackingNumber { get; set; }
        public string LabelUrl { get; set; }
    }
}
