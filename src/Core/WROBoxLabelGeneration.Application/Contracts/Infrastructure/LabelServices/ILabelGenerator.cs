namespace WROBoxLabelGeneration.Application.Contracts.Infrastructure.LabelServices
{
    public interface ILabelGenerator
    {
        Task<byte[]> CreatePdfAsBytes(object data);

        Task CreatePdfAsFile(object data, string path);
    }
}
