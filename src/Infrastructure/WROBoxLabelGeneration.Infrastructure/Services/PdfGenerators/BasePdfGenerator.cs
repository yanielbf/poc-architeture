namespace WROBoxLabelGeneration.Infrastructure.Services.PdfGenerators
{
    public abstract class BasePdfGenerator
    {
        public abstract void SaveAsFileFromHtml(string html, string path);

        public abstract byte[] SaveAsDataBytesFromHtml(string html);
    }
}
