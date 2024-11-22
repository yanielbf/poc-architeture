namespace WROBoxLabelGeneration.Infrastructure.Services.PdfGenerators
{
    public abstract class BasePdfGenerator
    {
        public abstract Task SaveAsFileFromHtml(string html, string path);

        public abstract Task<byte[]> SaveAsDataBytesFromHtml(string html);
    }
}
