using WePromoLink.DTO;
using WePromoLink.Models;

namespace WePromoLink.Extension;
public static class ImageDataExtensions
{
    public static ImageData ConvertToImageData(this ImageDataModel imageDataModel)
    {
        return new ImageData
        {
            ExternalId = imageDataModel.ExternalId,
            Compressed = imageDataModel.Compressed,
            CompressedAspectRatio = imageDataModel.CompressedAspectRatio,
            CompressedHeight = imageDataModel.CompressedHeight,
            CompressedWidth = imageDataModel.CompressedWidth,
            Medium = imageDataModel.Medium,
            MediumAspectRatio = imageDataModel.MediumAspectRatio,
            MediumHeight = imageDataModel.MediumHeight,
            MediumWidth = imageDataModel.MediumWidth,
            Original = imageDataModel.Original,
            OriginalAspectRatio = imageDataModel.OriginalAspectRatio,
            OriginalHeight = imageDataModel.OriginalHeight,
            OriginalWidth = imageDataModel.OriginalWidth,
            Thumbnail = imageDataModel.Thumbnail,
            ThumbnailAspectRatio = imageDataModel.ThumbnailAspectRatio,
            ThumbnailHeight = imageDataModel.ThumbnailHeight,
            ThumbnailWidth = imageDataModel.ThumbnailWidth
        };
    }
}
