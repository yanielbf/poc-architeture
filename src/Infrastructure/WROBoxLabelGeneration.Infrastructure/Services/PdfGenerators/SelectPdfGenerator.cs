using SelectPdf;

namespace WROBoxLabelGeneration.Infrastructure.Services.PdfGenerators
{
    public class SelectPdfGenerator : BasePdfGenerator
    {
        private SelectPdf.HtmlToPdf _converter;

        public SelectPdfGenerator()
        {
            _converter = new SelectPdf.HtmlToPdf();
            _converter.Options.PdfPageSize = PdfPageSize.Custom;
            _converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            _converter.Options.WebPageWidth = 840;
            _converter.Options.WebPageHeight = 996;
            _converter.Options.MarginTop = 50;
            _converter.Options.MarginBottom = 30;
            _converter.Options.MarginLeft = 30;
            _converter.Options.MarginRight = 30;
        }

        public override void SaveAsFileFromHtml(string html, string path)
        {
            SelectPdf.PdfDocument pdf = _converter.ConvertHtmlString(html);
            pdf.Save(path);
            pdf.Close();
        }

        public override byte[] SaveAsDataBytesFromHtml(string html)
        {
            SelectPdf.PdfDocument pdf = _converter.ConvertHtmlString(html);
            return pdf.Save();
        }
    }
}
