using Azure.Storage.Blobs;
using WePromoLink.Data;

namespace WePromoLink.Controller.Tasks;

public class CleanImagesTask
{
    private readonly IServiceScopeFactory _fac;
    private readonly ILogger<CleanImagesTask> _logger;

    public CleanImagesTask(IServiceScopeFactory fac, ILogger<CleanImagesTask> logger)
    {
        _fac = fac;
        _logger = logger;
    }

    public async Task Update()
    {
        using var scope = _fac.CreateScope();
        var _db = scope.ServiceProvider.GetRequiredService<DataContext>();
        var _blobContainerClient = scope.ServiceProvider.GetRequiredService<BlobServiceClient>();
        var containers = new List<string>
        {
            "compressed",
            "medium",
            "original",
            "thumbnail"
        };

        var images = _db.ImageDatas.Where(e => !e.Bound).ToList();
        foreach (var imageModel in images)
        {
            try
            {
                foreach (string containerName in containers)
                {
                    var img = imageModel.Original.Split('/').Reverse().ElementAt(0);
                    BlobContainerClient containerClient = _blobContainerClient.GetBlobContainerClient(containerName);
                    foreach (var item in containerClient.GetBlobsByHierarchy(delimiter: "/", prefix: img))
                    {
                        BlobClient blobClient = containerClient.GetBlobClient(item.Blob.Name);
                        await blobClient.DeleteAsync();
                        _logger.LogInformation($"Imagen {item.Blob.Name} eliminada del contenedor {containerName}");
                    }
                }
                _db.ImageDatas.Remove(imageModel);
                await _db.SaveChangesAsync();
                _logger.LogInformation($"ImagenModel {imageModel.Original} eliminada de la Base de datos");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"No se pudo eliminar la imagen {imageModel.Original}: {ex.Message}");
            }
        }
    }


}