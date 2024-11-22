using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace WROBoxLabelGeneration.Infrastructure.Services.PdfGenerators
{
    public class PuppeterPdfGenerator : BasePdfGenerator
    {
        private readonly BrowserFetcher _browserFetcher;
        private readonly PdfOptions _pdfOptions;

        public PuppeterPdfGenerator()
        {
            _browserFetcher = new BrowserFetcher();
            _pdfOptions = new PdfOptions();
            _pdfOptions.Format = new PaperFormat((decimal) 8.26, (decimal) 11.69);
            _pdfOptions.MarginOptions = new MarginOptions
            {
                Top = 50,
                Right = 30,
                Left = 30,
                Bottom = 30,
            };
        }

        public async override Task<byte[]> SaveAsDataBytesFromHtml(string html)
        {
            await _browserFetcher.DownloadAsync();
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            await using var page = await browser.NewPageAsync();
            await page.SetContentAsync(html);
            return await page.PdfDataAsync(_pdfOptions);
        }

        public async override Task<string> SaveAsFileFromHtml(string html, string path)
        {
            await _browserFetcher.DownloadAsync();
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            await using var page = await browser.NewPageAsync();
            await page.SetContentAsync(html);
            await page.PdfAsync(path, _pdfOptions);
            return path;
        }
    }
}
