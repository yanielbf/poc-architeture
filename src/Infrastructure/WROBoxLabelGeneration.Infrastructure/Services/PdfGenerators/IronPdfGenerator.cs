namespace WROBoxLabelGeneration.Infrastructure.Services.PdfGenerators
{
    public class IronPdfGenerator : BasePdfGenerator
    {
        private readonly ChromePdfRenderer _converter;
        public IronPdfGenerator()
        {
            _converter = new ChromePdfRenderer();
            _converter.RenderingOptions.MarginTop = 13;
            _converter.RenderingOptions.MarginLeft = 7;
            _converter.RenderingOptions.MarginRight = 7;
            _converter.RenderingOptions.MarginBottom = 7;
        }

        public override Task<string> SaveAsFileFromHtml(string html, string path)
        {
            var pdf = _converter.RenderHtmlAsPdf(html);
            pdf.SaveAs(path);
            return Task.FromResult(path);
        }

        public override Task<byte[]> SaveAsDataBytesFromHtml(string html)
        {
            var pdf = _converter.RenderHtmlAsPdf(html);
            return Task.FromResult(pdf.BinaryData);
        }
    }
}
