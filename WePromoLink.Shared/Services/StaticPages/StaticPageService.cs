using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NameCheap;
using Org.BouncyCastle.Asn1.Misc;
using WePromoLink.Data;
using WePromoLink.DTO.StaticPage;
using WePromoLink.Models;

namespace WePromoLink.Services.StaticPages;

public class StaticPageService : IStaticPageService
{
    private const string STATIC_PAGE_RESOURCES = "staticpageresource";
    private readonly DataContext _db;
    private readonly NameCheapApi _api;
    private readonly IConfiguration _config;
    private readonly BlobServiceClient _blobService;
    public StaticPageService(DataContext db, IConfiguration config, BlobServiceClient blobService)
    {
        _config = config;
        _db = db;
        _api = new NameCheapApi(
            _config["NameCheap:ApiUser"],
            _config["NameCheap:ApiUser"],
            _config["NameCheap:ApiKey"],
            _config["NameCheap:ClientIP"]);
        _blobService = blobService;
    }

    private async Task<string> UploadResourceAzure(IFormFile file, string? contentType = "")
    {
        BlobContainerClient containerClient = _blobService.GetBlobContainerClient(STATIC_PAGE_RESOURCES);
        if (!await containerClient.ExistsAsync()) await containerClient.CreateAsync(PublicAccessType.BlobContainer);
        var fileName = $"resource{Nanoid.Nanoid.Generate("0123456789", 15)}";

        BlobClient blobClient = containerClient.GetBlobClient(fileName);
        using (var stream = file.OpenReadStream())
        {
            if (String.IsNullOrEmpty(contentType))
            {
                await blobClient.UploadAsync(stream, true);
            }
            else
            {
                var options = new BlobUploadOptions();
                options.HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = contentType
                };
                await blobClient.UploadAsync(stream, options);
            }


        }
        return blobClient.Uri.ToString();
    }

    private async Task EditResourceAzure(string url, IFormFile newFile)
    {
        BlobContainerClient containerClient = _blobService.GetBlobContainerClient(STATIC_PAGE_RESOURCES);
        if (!await containerClient.ExistsAsync()) return;

        var blobName = url.Split('/').Reverse().ElementAt(0);
        BlobClient blobClient = containerClient.GetBlobClient(blobName);
        using (var stream = newFile.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, true);
        }
    }


    private async Task<bool> DeleteResourceAzure(string url)
    {
        BlobContainerClient containerClient = _blobService.GetBlobContainerClient(STATIC_PAGE_RESOURCES);
        if (!await containerClient.ExistsAsync()) return false;

        var blobName = url.Split('/').Reverse().ElementAt(0);
        BlobClient blobClient = containerClient.GetBlobClient(blobName);
        if (await blobClient.ExistsAsync())
        {
            await blobClient.DeleteAsync();
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<Guid> CreateStaticPage(StaticPageCreate data)
    {
        var exits = await _db.StaticPages.AnyAsync(e => e.Name.ToLower() == data.Name.ToLower());
        if (exits) throw new Exception("Static Page already exits");

        var dnsHostResult = _api.Dns.GetHosts(data.SLD, data.TLD);
        var existHostEntry = dnsHostResult.HostEntries.Any(e => e.RecordType == RecordType.A && e.HostName.ToLower() == data.Name.ToLower());
        if (existHostEntry) throw new Exception("DNS Record already exits");

        var entries = dnsHostResult.HostEntries.ToList();
        entries.Add(new HostEntry
        {
            RecordType = RecordType.A,
            HostName = data.Name.ToLower(),
            Address = data.IP
        });

        _api.Dns.SetHosts(data.SLD.ToLower(), data.TLD.ToLower(), entries.ToArray());

        var pageId = Guid.NewGuid();
        _db.StaticPages.Add(new Models.StaticPageModel
        {
            CreatedAt = DateTime.UtcNow,
            Etag = Nanoid.Nanoid.Generate(size: 12),
            ExpiredAt = DateTime.UtcNow.AddYears(5),
            Id = pageId,
            IP = data.IP,
            LastModified = DateTime.UtcNow,
            MaxAge = TimeSpan.FromHours(10),
            Name = $"{data.Name.ToLower()}.{data.SLD.ToLower()}.{data.TLD.ToLower()}",
            StaticPageDataTemplateModelId = data.DataTemplateId,
            StaticPageWebsiteTemplateModelId = data.WebsiteTemplateId
        });

        _db.SaveChanges();
        return pageId;
    }

    public async Task<Guid> CreateStaticPageDataTemplate(StaticPageDataTemplateCreate data)
    {
        var id = Guid.NewGuid();
        _db.StaticPageDataTemplates.Add(new StaticPageDataTemplateModel
        {
            CreatedAt = DateTime.UtcNow,
            Etag = Nanoid.Nanoid.Generate(size: 12),
            ExpiredAt = DateTime.UtcNow.AddYears(5),
            Id = id,
            MaxAge = TimeSpan.FromHours(10),
            LastModified = DateTime.UtcNow,
            Name = data.Name,
            Json = await UploadResourceAzure(data.File)
        });
        _db.SaveChanges();
        return id;
    }

    public async Task<Guid> CreateStaticPageProduct(StaticPageProductCreate data)
    {
        var id = Guid.NewGuid();
        await _db.StaticPageProducts.AddAsync(new StaticPageProductModel
        {
            Id = id,
            AffiliateProgram = data.AffiliateProgram,
            BuyLink = data.BuyLink,
            Category = data.Category,
            AffiliateLink = data.AffiliateLink,
            Commission = data.Commission,
            CostPrice = data.CostPrice,
            Description = data.Description,
            Discount = data.Discount,
            Height = data.Height,
            Width = data.Width,
            Inventory = data.Inventory,
            Length = data.Length,
            Price = data.Price,
            Provider = data.Provider,
            SKU = data.SKU,
            Tags = data.Tags,
            Title = data.Title,
            Weight = data.Weight
        });
        _db.SaveChanges();
        return id;
    }

    public async Task<Guid> CreateStaticPageProductByPage(StaticPageProductByPageCreate data)
    {
        var id = Guid.NewGuid();
        await _db.StaticPageProductByPages.AddAsync(new StaticPageProductByPageModel
        {
            Id = id,
            StaticPageModelId = data.StaticPagePageModelId,
            StaticPageProductModelId = data.StaticPageProductModelId
        });
        _db.SaveChanges();
        return id;
    }

    public async Task<Guid> CreateStaticPageProductByResource(StaticPageProductByResourceCreate data)
    {
        var id = Guid.NewGuid();
        await _db.StaticPageProductByResources.AddAsync(new StaticPageProductByResourceModel
        {
            Id = id,
            StaticPageProductModelId = data.StaticPageProductModelId,
            StaticPageResourceModelId = data.StaticPageResourceModelId
        });
        _db.SaveChanges();
        return id;
    }

    public async Task<Guid> CreateStaticPageResource(StaticPageResourceCreate data)
    {
        var id = Guid.NewGuid();
        await _db.StaticPageResources.AddAsync(new StaticPageResourceModel
        {
            ContentType = data.ContentType,
            Height = data.Height,
            Id = id,
            Name = data.Name,
            SizeMB = data.SizeMB,
            Width = data.Height,
            Url = await UploadResourceAzure(data.File)
        });
        _db.SaveChanges();
        return id;
    }

    public async Task<Guid> CreateStaticPageWebsiteTemplate(StaticPageWebsiteTemplateCreate data)
    {
        var id = Guid.NewGuid();
        await _db.StaticPageWebsiteTemplates.AddAsync(new StaticPageWebsiteTemplateModel
        {
            Id = id,
            CreatedAt = DateTime.UtcNow,
            Etag = Nanoid.Nanoid.Generate(size: 12),
            ExpiredAt = DateTime.UtcNow.AddDays(30),
            LastModified = DateTime.UtcNow,
            MaxAge = TimeSpan.FromHours(10),
            Name = data.Name,
            Url = await UploadResourceAzure(data.File)
        });
        _db.SaveChanges();
        return id;
    }

    public async Task<bool> DeleteStaticPage(Guid id)
    {
        var page = await _db.StaticPages.Where(e => e.Id == id).SingleOrDefaultAsync();
        if (page == null) throw new Exception("Static Page does not exits");

        string[] partes = page.Name.ToLower().Split('.');
        var hostname = string.Join(".", partes, 0, partes.Length - 2);
        var sld = partes[partes.Length - 2];
        var tld = partes[partes.Length - 1];

        var dnsHostResult = _api.Dns.GetHosts(sld, tld);
        var existHostEntry = dnsHostResult.HostEntries.Any(e => e.RecordType == RecordType.A && e.HostName.ToLower() == hostname);
        if (!existHostEntry) throw new Exception("DNS Record does not exits");

        var list = dnsHostResult.HostEntries.ToList();
        list.Remove(list.Find(e => e.RecordType == RecordType.A && e.HostName == hostname));
        _api.Dns.SetHosts(sld, tld, list.ToArray());
        _db.StaticPages.Remove(page);
        _db.SaveChanges();
        return true;
    }

    public async Task<bool> DeleteStaticPageDataTemplate(Guid id)
    {
        var item = await _db.StaticPageDataTemplates.Where(e => e.Id == id).SingleOrDefaultAsync();
        if (item == null) throw new Exception("Element not found");
        await DeleteResourceAzure(item.Json);
        _db.StaticPageDataTemplates.Remove(item);
        _db.SaveChanges();
        return true;
    }

    public async Task<bool> DeleteStaticPageProduct(Guid id)
    {
        var item = await _db.StaticPageProducts.Where(e => e.Id == id).SingleOrDefaultAsync();
        if (item == null) throw new Exception("Element not found");
        _db.StaticPageProducts.Remove(item);
        _db.SaveChanges();
        return true;
    }

    public async Task<bool> DeleteStaticPageProductByPage(Guid id)
    {
        var item = await _db.StaticPageProductByPages.Where(e => e.Id == id).SingleOrDefaultAsync();
        if (item == null) throw new Exception("Element not found");
        _db.StaticPageProductByPages.Remove(item);
        _db.SaveChanges();
        return true;
    }

    public async Task<bool> DeleteStaticPageProductByResource(Guid id)
    {
        var item = await _db.StaticPageProductByResources.Where(e => e.Id == id).SingleOrDefaultAsync();
        if (item == null) throw new Exception("Element not found");
        _db.StaticPageProductByResources.Remove(item);
        _db.SaveChanges();
        return true;
    }

    public async Task<bool> DeleteStaticPageResource(Guid id)
    {
        var item = await _db.StaticPageResources.Where(e => e.Id == id).SingleOrDefaultAsync();
        if (item == null) throw new Exception("Element not found");
        await DeleteResourceAzure(item.Url);
        _db.StaticPageResources.Remove(item);
        _db.SaveChanges();
        return true;
    }

    public async Task<bool> DeleteStaticPageWebsiteTemplate(Guid id)
    {
        var item = await _db.StaticPageWebsiteTemplates.Where(e => e.Id == id).SingleOrDefaultAsync();
        if (item == null) throw new Exception("Element not found");
        await DeleteResourceAzure(item.Url);
        _db.StaticPageWebsiteTemplates.Remove(item);
        _db.SaveChanges();
        return true;
    }

    public async Task<Guid> EditStaticPage(StaticPageEdit data)
    {
        var item = await _db.StaticPages.Where(e => e.Id == data.Id).SingleOrDefaultAsync();
        if (item == null) throw new Exception("Element not found");

        item.Etag = Nanoid.Nanoid.Generate(size: 12);
        item.ExpiredAt = DateTime.UtcNow.AddDays(30);
        item.LastModified = DateTime.UtcNow;
        item.StaticPageDataTemplateModelId = data.DataTemplateId;
        item.StaticPageWebsiteTemplateModelId = data.WebsiteTemplateId;

        _db.StaticPages.Update(item);
        _db.SaveChanges();
        return item.Id;
    }

    public async Task<Guid> EditStaticPageDataTemplate(StaticPageDataTemplateEdit data)
    {
        var item = await _db.StaticPageDataTemplates.Where(e => e.Id == data.Id).SingleOrDefaultAsync();
        if (item == null) throw new Exception("Element not found");

        item.Etag = Nanoid.Nanoid.Generate(size: 12);
        item.ExpiredAt = DateTime.UtcNow.AddDays(30);
        item.LastModified = DateTime.UtcNow;
        item.Name = data.Name;
        if (data.File != null)
        {
            await EditResourceAzure(item.Json, data.File);
        }
        _db.StaticPageDataTemplates.Update(item);
        _db.SaveChanges();
        return item.Id;
    }

    public async Task<Guid> EditStaticPageProduct(StaticPageProductEdit data)
    {
        var item = await _db.StaticPageProducts.Where(e => e.Id == data.Id).SingleOrDefaultAsync();
        if (item == null) throw new Exception("Element not found");

        item.AffiliateLink = data.AffiliateLink;
        item.AffiliateProgram = data.AffiliateProgram;
        item.BuyLink = data.BuyLink;
        item.Category = data.Category;
        item.Commission = data.Commission;
        item.CostPrice = data.CostPrice;
        item.Description = data.Description;
        item.Discount = data.Discount;
        item.Height = data.Height;
        item.Inventory = data.Inventory;
        item.Length = data.Length;
        item.Price = data.Price;
        item.Provider = data.Provider;
        item.SKU = data.SKU;
        item.Tags = data.Tags;
        item.Title = data.Title;
        item.Weight = data.Weight;
        item.Width = data.Width;

        _db.StaticPageProducts.Update(item);
        _db.SaveChanges();
        return item.Id;
    }

    public async Task<Guid> EditStaticPageResource(StaticPageResourceEdit data)
    {
        var item = await _db.StaticPageResources.Where(e => e.Id == data.Id).SingleOrDefaultAsync();
        if (item == null) throw new Exception("Element not found");

        item.ContentType = data.ContentType;
        item.Height = data.Height;
        item.Name = data.Name;
        item.SizeMB = data.SizeMB;
        item.Width = data.Width;
        if (data.File != null)
        {
            await EditResourceAzure(item.Url, data.File);
        }
        _db.StaticPageResources.Update(item);
        _db.SaveChanges();
        return item.Id;
    }

    public async Task<Guid> EditStaticPageWebsiteTemplate(StaticPageWebsiteTemplateEdit data)
    {
        var item = await _db.StaticPageWebsiteTemplates.Where(e => e.Id == data.Id).SingleOrDefaultAsync();
        if (item == null) throw new Exception("Element not found");

        item.Name = data.Name;
        item.Etag = Nanoid.Nanoid.Generate(size: 12);
        item.ExpiredAt = DateTime.UtcNow.AddDays(30);
        item.LastModified = DateTime.UtcNow;
        if (data.File != null)
        {
            await EditResourceAzure(item.Url, data.File);
        }
        _db.StaticPageWebsiteTemplates.Update(item);
        _db.SaveChanges();
        return item.Id;
    }

    public async Task<bool> ExitsStaticPageProductByPage(StaticPageProductByPageCreate data)
    {
        return await _db.StaticPageProductByPages
        .AnyAsync(e =>
        e.StaticPageModelId == data.StaticPagePageModelId &&
        e.StaticPageProductModelId == data.StaticPageProductModelId);
    }

    public async Task<bool> ExitsStaticPageProductByResource(StaticPageProductByResourceCreate data)
    {
        return await _db.StaticPageProductByResources
        .AnyAsync(e =>
        e.StaticPageResourceModelId == data.StaticPageResourceModelId &&
        e.StaticPageProductModelId == data.StaticPageProductModelId);
    }

    public async Task<PaginationList<StaticPageRead>> GetAllStaticPage(int page = 1, int cant = 25, string filter = "")
    {
        return await PaginationUtil.Pagination<StaticPageModel, StaticPageRead>(
            _db.StaticPages
            .Include(e => e.StaticPageDataTemplate)
            .Include(e => e.StaticPageWebsiteTemplate),
            e => new StaticPageRead
            {
                Id = e.Id,
                IP = e.IP,
                DataTemplateId = e.StaticPageDataTemplateModelId,
                DataTemplateUrl = e.StaticPageDataTemplate.Json,
                DataTemplateName = e.StaticPageDataTemplate.Name,
                Name = e.Name,
                WebsiteTemplateId = e.StaticPageWebsiteTemplateModelId,
                WebsiteTemplateName = e.StaticPageWebsiteTemplate.Name,
                WebsiteTemplateUrl = e.StaticPageWebsiteTemplate.Url
            }
        , page, cant, filter);
    }

    public async Task<PaginationList<StaticPageDataTemplateRead>> GetAllStaticPageDataTemplate(int page = 1, int cant = 25, string filter = "")
    {
        return await PaginationUtil.Pagination<StaticPageDataTemplateModel, StaticPageDataTemplateRead>(
            _db.StaticPageDataTemplates,
            e => new StaticPageDataTemplateRead
            {
                Id = e.Id,
                Name = e.Name,
                DataTemplateJsonUrl = e.Json
            }
        , page, cant, filter);
    }

    public async Task<PaginationList<StaticPageProductRead>> GetAllStaticPageProduct(int page = 1, int cant = 25, string filter = "")
    {
        string[] imageContentTypes = new string[]
        {
            "image/jpeg",
            "image/png",
            "image/gif",
            "image/bmp",
            "image/tiff",
        };

        var getImages = (Guid id) =>
        {
            return _db.StaticPageProductByResources
                    .Include(e => e.Resource)
                    .Where(e => e.StaticPageProductModelId == id)
                    .Where(e => imageContentTypes.Contains(e.Resource.ContentType.ToLower()))
                    .Select(e => e.Resource.Url)
                    .ToList();
        };

        var result = await PaginationUtil.Pagination<StaticPageProductModel, StaticPageProductRead>(
            _db.StaticPageProducts,
            e => new StaticPageProductRead
            {
                Id = e.Id,
                AffiliateLink = e.AffiliateLink,
                AffiliateProgram = e.AffiliateProgram,
                BuyLink = e.BuyLink,
                Category = e.Category,
                Commission = e.Commission,
                CostPrice = e.CostPrice,
                Description = e.Description,
                Discount = e.Discount,
                Height = e.Height,
                Inventory = e.Inventory,
                Length = e.Length,
                Price = e.Price,
                Provider = e.Provider,
                SKU = e.SKU,
                Tags = e.Tags,
                Title = e.Title,
                Weight = e.Weight,
                Width = e.Width
            }
        , page, cant, filter);

        foreach (var item in result.Items)
        {
            item.ImagesUrl = getImages(item.Id);
        }

        return result;
    }

    public async Task<PaginationList<StaticPageResourceRead>> GetAllStaticPageResource(int page = 1, int cant = 25, string filter = "")
    {
        return await PaginationUtil.Pagination<StaticPageResourceModel, StaticPageResourceRead>(
            _db.StaticPageResources,
            e => new StaticPageResourceRead
            {
                Id = e.Id,
                Height = e.Height,
                Width = e.Width,
                ContentType = e.ContentType,
                Name = e.Name,
                SizeMB = e.SizeMB,
                Url = e.Url
            }
        , page, cant, filter);
    }

    public async Task<PaginationList<StaticPageWebsiteTemplateRead>> GetAllStaticPageWebsiteTemplate(int page = 1, int cant = 25, string filter = "")
    {
        return await PaginationUtil.Pagination<StaticPageWebsiteTemplateModel, StaticPageWebsiteTemplateRead>(
            _db.StaticPageWebsiteTemplates,
            e => new StaticPageWebsiteTemplateRead
            {
                Id = e.Id,
                Name = e.Name,
                WebsiteTemplateUrl = e.Url
            }
        , page, cant, filter);
    }

    public async Task<StaticPageRead> GetStaticPage(Guid id)
    {
        return await _db.StaticPages
        .Where(e => e.Id == id)
        .Include(e => e.StaticPageDataTemplate)
        .Include(e => e.StaticPageWebsiteTemplate)
        .Select(e => new StaticPageRead
        {
            Id = e.Id,
            IP = e.IP,
            DataTemplateId = e.StaticPageDataTemplateModelId,
            DataTemplateUrl = e.StaticPageDataTemplate.Json,
            DataTemplateName = e.StaticPageDataTemplate.Name,
            Name = e.Name,
            WebsiteTemplateId = e.StaticPageWebsiteTemplateModelId,
            WebsiteTemplateName = e.StaticPageWebsiteTemplate.Name,
            WebsiteTemplateUrl = e.StaticPageWebsiteTemplate.Url
        })
        .SingleAsync();
    }

    public async Task<StaticPageDataTemplateRead> GetStaticPageDataTemplate(Guid id)
    {
        return await _db.StaticPageDataTemplates
        .Where(e => e.Id == id)
        .Select(e => new StaticPageDataTemplateRead
        {
            Id = e.Id,
            Name = e.Name,
            DataTemplateJsonUrl = e.Json
        })
        .SingleAsync();
    }

    public async Task<StaticPageProductRead> GetStaticPageProduct(Guid id)
    {
        string[] imageContentTypes = new string[]
        {
            "image/jpeg",
            "image/png",
            "image/gif",
            "image/bmp",
            "image/tiff",
        };

        var imagesUrls = _db.StaticPageProductByResources
        .Include(e => e.Resource)
        .Where(e => e.StaticPageProductModelId == id)
        .Where(e => imageContentTypes.Contains(e.Resource.ContentType.ToLower()))
        .Select(e => e.Resource.Url)
        .ToList();

        return await _db.StaticPageProducts
        .Where(e => e.Id == id)
        .Select(e => new StaticPageProductRead
        {
            Id = e.Id,
            AffiliateLink = e.AffiliateLink,
            AffiliateProgram = e.AffiliateProgram,
            BuyLink = e.BuyLink,
            Category = e.Category,
            Commission = e.Commission,
            CostPrice = e.CostPrice,
            Description = e.Description,
            Discount = e.Discount,
            Height = e.Height,
            Inventory = e.Inventory,
            Length = e.Length,
            Price = e.Price,
            Provider = e.Provider,
            SKU = e.SKU,
            Tags = e.Tags,
            Title = e.Title,
            Weight = e.Weight,
            Width = e.Width,
            ImagesUrl = imagesUrls
        })
        .SingleAsync();
    }

    public async Task<StaticPageResourceRead> GetStaticPageResource(Guid id)
    {
        return await _db.StaticPageResources
        .Where(e => e.Id == id)
        .Select(e => new StaticPageResourceRead
        {
            Id = e.Id,
            Height = e.Height,
            Width = e.Width,
            ContentType = e.ContentType,
            Name = e.Name,
            SizeMB = e.SizeMB,
            Url = e.Url
        })
        .SingleAsync();
    }

    public async Task<StaticPageWebsiteTemplateRead> GetStaticPageWebsiteTemplate(Guid id)
    {
        return await _db.StaticPageWebsiteTemplates
        .Where(e => e.Id == id)
        .Select(e => new StaticPageWebsiteTemplateRead
        {
            Id = e.Id,
            Name = e.Name,
            WebsiteTemplateUrl = e.Url
        })
        .SingleAsync();
    }
}
