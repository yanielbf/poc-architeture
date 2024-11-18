namespace WROBoxLabelGeneration.Models
{
    public class WroInventoryPackaging
    {
        public int Id { get; private set; }
        public int InventoryDetailsId { get; private set; }
        public int PackagingDetailsId { get; private set; }
        public int ItemQuantity { get; private set; }
        public int? ReceivedQuantity { get; private set; }
        public int? StowedQuantity { get; private set; }
        public WroPackagingDetail WroPackagingDetail { get; private set; }
    }
}
