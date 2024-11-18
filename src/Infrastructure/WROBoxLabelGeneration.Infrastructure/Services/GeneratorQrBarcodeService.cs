using BarcodeStandard;
using SkiaSharp;
using System.Drawing;
using System.Net;
using ZXing;
using ZXing.QrCode;
using ZXing.Windows.Compatibility;

namespace WROBoxLabelGeneration.Infrastructure.Services
{
    public class GeneratorQrBarcodeService
    {
        public string QrCodeBase64Image(string text)
        {
            var data = GenerateQrCode(text);
            return $"data:image/png;base64,{Convert.ToBase64String(data)}";
        }

        public string BarcodeBase64Image(string text, bool includeLabel = true, bool positionToLeft = false)
        {
            var data = GenerateBarcode(text, includeLabel, positionToLeft);
            return $"data:image/png;base64,{Convert.ToBase64String(data)}";
        }

        public string ImageUrl64Image(string url)
        {
            var data = GetImageFromUrl(url);
            return $"data:image/png;base64,{Convert.ToBase64String(data)}";
        }

        private byte[] GenerateQrCode(string text)
        {
            QrCodeEncodingOptions options = new QrCodeEncodingOptions();
            options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 100,
                Height = 100,
            };
            var writer = new BarcodeWriter<Bitmap>();
            writer.Renderer = new BitmapRenderer();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            var img = new Bitmap(writer.Write(text));
            using (MemoryStream barcodeStream = new MemoryStream())
            {
                img.Save(barcodeStream, System.Drawing.Imaging.ImageFormat.Png);
                barcodeStream.Position = 0;
                return barcodeStream.ToArray();
            }
        }

        private byte[] GenerateBarcode(string text, bool includeLabel, bool positionToLeft)
        {
            var writer = new Barcode();
            writer.IncludeLabel = includeLabel;
            writer.Alignment = positionToLeft ? AlignmentPositions.Left : AlignmentPositions.Center;
            writer.LabelFont = new SKFont(SKTypeface.Default, 12);
            var img = writer.Encode(BarcodeStandard.Type.Code128, text, SKColors.Black, SKColors.White, 290, 120);
            using (var encoded = img.Encode(SKEncodedImageFormat.Png, 100))
            {
                using (MemoryStream barcodeStream = new MemoryStream(encoded.ToArray()))
                {
                    return barcodeStream.ToArray();
                }
            }
            
        }

        private byte[] GetImageFromUrl(string url)
        {
            WebClient client = new WebClient();
            return client.DownloadData(url);
        }
    }
}
