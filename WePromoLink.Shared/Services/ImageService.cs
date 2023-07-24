using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using WePromoLink.Data;
using WePromoLink.DTO;
using WePromoLink.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Fonts;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Drawing.Processing;

namespace WePromoLink.Services
{
    public class ImageService : IImageService
    {
        private readonly ILogger<ImageService> _logger;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly DataContext _db;

        public ImageService(BlobServiceClient blobServiceClient, DataContext db, ILogger<ImageService> logger)
        {
            _blobServiceClient = blobServiceClient;
            _db = db;
            _logger = logger;
        }

        public async Task<string> ProcessImage(IFormFile image)
        {
            using (var trans = _db.Database.BeginTransaction())
            {
                try
                {
                    var result = await ProcessImageAndUpload(image);

                    _db.ImageDatas.Add(new ImageDataModel
                    {
                        Id = Guid.NewGuid(),
                        Compressed = result.Compressed,
                        CompressedAspectRatio = result.CompressedAspectRatio,
                        CompressedHeight = result.CompressedHeight,
                        CompressedWidth = result.CompressedWidth,
                        ExternalId = result.ExternalId,
                        Medium = result.Medium,
                        MediumAspectRatio = result.MediumAspectRatio,
                        MediumHeight = result.MediumHeight,
                        MediumWidth = result.MediumWidth,
                        Original = result.Original,
                        OriginalAspectRatio = result.OriginalAspectRatio,
                        OriginalHeight = result.OriginalHeight,
                        OriginalWidth = result.OriginalWidth,
                        Thumbnail = result.Thumbnail,
                        ThumbnailAspectRatio = result.ThumbnailAspectRatio,
                        ThumbnailHeight = result.ThumbnailHeight,
                        ThumbnailWidth = result.ThumbnailWidth,
                        Bound = false
                    });

                    await _db.SaveChangesAsync();
                    await trans.CommitAsync();
                    return result.ExternalId;
                }
                catch (System.Exception ex)
                {
                    trans.Rollback();
                    _logger.LogError(ex.Message);
                    throw;
                }
            }
        }

        private async Task<ImageData> ProcessImageAndUpload(IFormFile image)
        {
            // Crear un nombre único para el archivo
            string ext = Path.GetExtension(image.FileName);
            string uniqueFileName = $"image{Nanoid.Nanoid.Generate("0123456789", 15)}{ext}";

            // Guardar la imagen original en Azure Blob Storage
            string originalUrl = await UploadImageToBlobStorage(image, uniqueFileName, "original");

            // Obtener las dimensiones de la imagen original
            using (var originalImage = Image.Load(image.OpenReadStream()))
            {
                int originalWidth = originalImage.Width;
                int originalHeight = originalImage.Height;
                double originalAspectRatio = (double)originalWidth / originalHeight;

                // Crear una versión comprimida de la imagen original
                string compressedUrl = await CreateCompressedImage(originalImage, uniqueFileName, "compressed", ext.ToLower() == ".png");

                // Crear una versión de tamaño mediano de la imagen original
                string mediumUrl = await CreateResizedImage(originalImage, uniqueFileName, Convert.ToInt32(400 * originalAspectRatio), 400, "medium");

                // Crear una miniatura de la imagen original
                string thumbnailUrl = await CreateResizedImage(originalImage, uniqueFileName, Convert.ToInt32(80 * originalAspectRatio), 80, "thumbnail");

                return new ImageData
                {
                    ExternalId = Nanoid.Nanoid.Generate(size: 12),
                    Original = originalUrl,
                    OriginalWidth = originalWidth,
                    OriginalHeight = originalHeight,
                    OriginalAspectRatio = originalAspectRatio,
                    Compressed = compressedUrl,
                    CompressedWidth = originalWidth,
                    CompressedHeight = originalHeight,
                    CompressedAspectRatio = originalAspectRatio,
                    Medium = mediumUrl,
                    MediumWidth = Convert.ToInt32(400 * originalAspectRatio),
                    MediumHeight = 400,
                    MediumAspectRatio = originalAspectRatio,
                    Thumbnail = thumbnailUrl,
                    ThumbnailWidth = Convert.ToInt32(80 * originalAspectRatio),
                    ThumbnailHeight = 80,
                    ThumbnailAspectRatio = originalAspectRatio
                };
            }
        }

        private async Task<string> UploadImageToBlobStorage(IFormFile image, string fileName, string container)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(container);

            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            using (var stream = image.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            return blobClient.Uri.ToString();
        }

        private async Task<string> CreateCompressedImage(Image image, string fileName, string container, bool isPNG)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(container);

            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            using (var outputStream = new MemoryStream())
            {
                ApplyWaterMark(image); // Apply watermark
                AddWaterMarkURL(image); 
                image.SaveAsJpeg(outputStream, new JpegEncoder { Quality = 50 });
                outputStream.Position = 0;
                await blobClient.UploadAsync(outputStream, true);
            }
            return blobClient.Uri.ToString();
        }

        private async Task<string> CreateResizedImage(Image image, string fileName, int width, int height, string container)
        {
            string resizedFileName = $"{fileName}_{width}x{height}.jpg";
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(container);

            BlobClient blobClient = containerClient.GetBlobClient(resizedFileName);
            using (var outputStream = new MemoryStream())
            {

                image.Mutate(ctx => ctx.Resize(new ResizeOptions
                {
                    Mode = ResizeMode.BoxPad,
                    Size = new SixLabors.ImageSharp.Size(width, height)
                }));
                image.SaveAsJpeg(outputStream);
                outputStream.Position = 0;
                await blobClient.UploadAsync(outputStream, true);
            }
            return blobClient.Uri.ToString();
        }

        public async Task<ImageData> GetImage(string id)
        {
            return _db.ImageDatas.Where(e => e.ExternalId == id).Select(e => new ImageData
            {
                ExternalId = e.ExternalId,
                Compressed = e.Compressed,
                CompressedAspectRatio = e.CompressedAspectRatio,
                CompressedHeight = e.CompressedHeight,
                CompressedWidth = e.CompressedWidth,
                Medium = e.Medium,
                MediumAspectRatio = e.MediumAspectRatio,
                MediumHeight = e.MediumHeight,
                MediumWidth = e.MediumWidth,
                Original = e.Original,
                OriginalAspectRatio = e.OriginalAspectRatio,
                OriginalHeight = e.OriginalHeight,
                OriginalWidth = e.OriginalWidth,
                Thumbnail = e.Thumbnail,
                ThumbnailAspectRatio = e.ThumbnailAspectRatio,
                ThumbnailHeight = e.ThumbnailHeight,
                ThumbnailWidth = e.ThumbnailWidth
            }).Single();
        }

        private void ApplyWaterMark(Image image)
        {
            string logoFileName = "logo.png";
            string logoFilePath = Path.Combine(AppContext.BaseDirectory, "Assets", logoFileName);

            using (var logoStream = File.OpenRead(logoFilePath))
            {
                using (var logoImage = Image.Load(logoStream))
                {
                    // Calculate the position where the watermark will be placed (bottom-right corner in this example)
                    int posX = image.Width - logoImage.Width - 150; // 10-pixel padding from the right edge
                    int posY = image.Height - logoImage.Height - 35; // 10-pixel padding from the bottom edge

                    // Apply watermark (logo) on the image
                    image.Mutate(ctx => ctx.DrawImage(new Image<Rgba32>(logoImage.Width, logoImage.Height, Color.Transparent), new Point(posX, posY), 1f)); // Transparent background for the watermark
                    image.Mutate(ctx => ctx.DrawImage(logoImage, new Point(posX, posY), 0.5f)); // 0.5f is the opacity (0.0f to 1.0f)
                }
            }
        }

        private void AddWaterMarkURL(Image image)
        {
            string fontName = "audiowide.ttf";
            string fontFilePath = Path.Combine(AppContext.BaseDirectory, "Assets", fontName);

            FontCollection collection = new();
            collection.Add(fontFilePath);
            if (collection.TryGet("Audiowide", out FontFamily family))
            {
                Font font = family.CreateFont(40, FontStyle.Regular);
                var text = "wepromolink.com";

                int posX = image.Width - 400;
                int posY = image.Height - 60;
                var textColor = Color.FromRgba(255, 255, 255, 10);
                image.Mutate(ctx => ctx.DrawText(text, font, textColor, new Point(posX, posY))); // Place the text at the bottom center with 30-pixel padding
            }
        }
    }
}
