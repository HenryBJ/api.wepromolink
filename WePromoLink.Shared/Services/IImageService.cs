using Microsoft.AspNetCore.Http;
using WePromoLink.DTO;

namespace WePromoLink.Services;

public interface IImageService
{
    Task<string> ProcessImage(IFormFile image);
    Task<ImageData> GetImage(string id);
}