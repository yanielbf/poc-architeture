namespace WROBoxLabelGeneration.Models
{
    public class FulfillmentCenter
    {
        public int FulfillmentCenterId { get; private set; }
        public string CenterName { get; private set; }
        public string StreetAddress1 { get; private set; }
        public string StreetAddress2 { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
        public string Phone { get; private set; }
        public string Timezone { get; private set; }
        public string Email { get; private set; } = "support@shipbob.com";
        public short? FulfillmentCenterTypeId { get; private set; }
        public int CityId { get; private set; }
        public City City { get; private set; }
    }
}
