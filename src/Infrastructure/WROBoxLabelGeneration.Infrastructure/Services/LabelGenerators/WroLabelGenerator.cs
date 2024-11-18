using Microsoft.AspNetCore.Components.Web;
using WROBoxLabelGeneration.Infrastructure.Services.PdfGenerators;
using WROBoxLabelGeneration.Infrastructure.Services.LabelGenerators.Templates;
using WROBoxLabelGeneration.Models;
using Microsoft.AspNetCore.Components;

namespace WROBoxLabelGeneration.Infrastructure.Services.LabelGenerators
{
    public class WroLabelGenerator : BaseLabelGenerator
    {
        public WroLabelGenerator(HtmlRenderer render, BasePdfGenerator pdfGenerator) : base(render, pdfGenerator) {}

        public override async Task<byte[]> CreatePdfAsBytes(object data)
        {
            _html = await _render.Dispatcher.InvokeAsync(async () =>
            {
                var dictionary = new Dictionary<string, object?> 
                {
                    { "Wro", (Wro) data }
                };
                var output = await _render.RenderComponentAsync<WroLabelTemplate>(ParameterView.FromDictionary(dictionary));
                return output.ToHtmlString();
            });
            return _pdfGenerator.SaveAsDataBytesFromHtml(_html);
        }

        public override async Task CreatePdfAsFile(object data, string path)
        {
            _html = await _render.Dispatcher.InvokeAsync(async () =>
            {
                var output = await _render.RenderComponentAsync<WroLabelTemplate>();
                return output.ToHtmlString();
            });
            _pdfGenerator.SaveAsFileFromHtml(_html, path);
        }
    }
}
