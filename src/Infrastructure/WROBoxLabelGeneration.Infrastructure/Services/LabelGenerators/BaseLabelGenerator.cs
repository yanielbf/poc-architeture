using Microsoft.AspNetCore.Components.Web;
using SelectPdf;
using WROBoxLabelGeneration.Application.Contracts.Infrastructure.LabelServices;
using WROBoxLabelGeneration.Infrastructure.Services.PdfGenerators;

namespace WROBoxLabelGeneration.Infrastructure.Services.LabelGenerators
{
    public abstract class BaseLabelGenerator : ILabelGenerator 
    {
        protected string _html;

        protected readonly HtmlRenderer _render;
        protected readonly BasePdfGenerator _pdfGenerator;

        public BaseLabelGenerator(HtmlRenderer render, BasePdfGenerator basePdfGenerator)
        {
            _render = render;
            _pdfGenerator = basePdfGenerator;
        }

        public abstract Task<byte[]> CreatePdfAsBytes(object data);

        public abstract Task CreatePdfAsFile(object data, string path);
    }
}
