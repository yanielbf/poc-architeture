namespace WROBoxLabelGeneration.Models
{
    public class WroPackagingDetail
    {
        public int Id { get; private set; }
        public int BoxNumber { get; private set; }
        public WroInventoryPackaging WroInventoryPackaging { get; private set; }
    }
}
