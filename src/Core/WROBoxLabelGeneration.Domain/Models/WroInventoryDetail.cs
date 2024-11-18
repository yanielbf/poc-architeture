namespace WROBoxLabelGeneration.Models
{
    public class WroInventoryDetail
    {
        public int Id { get; private set; }
        public int Quantity { get; private set; }
        public string? LotNumber { get; private set; }
        public DateTime? LotDate { get; private set; }
        public int InventoryId { get; private set; }
        public Inventory Inventory { get; private set; }
        public int RequestId { get; private set; }
        public ICollection<WroInventoryPackaging> WroInventoryPackagings { get; private set; }
    }
}
