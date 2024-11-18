namespace WROBoxLabelGeneration.Domain.DTOs.HttpClients.Attachemnts
{
    public class CreateAttachmentRequestDTO
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}
