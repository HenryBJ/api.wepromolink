using WePromoLink.DTO.StaticPage;

namespace WePromoLink.Services.StaticPages;

public interface IStaticPageService
{
    Task<Guid> CreateStaticPage(StaticPageCreate data);
    Task<Guid> EditStaticPage(StaticPageEdit data);
    Task<PaginationList<StaticPageRead>> GetAllStaticPage(int page, int cant, string filter);
    Task<StaticPageRead> GetStaticPage(Guid id);
    Task<bool> DeleteStaticPage(Guid id);
    Task AddProduct(Guid pageId, Guid productId);
    Task RemoveProduct(Guid pageId, Guid productId);
    Task ClearCache(Guid pageId);

    Task<Guid> CreateStaticPageResource(StaticPageResourceCreate data);
    Task<Guid> EditStaticPageResource(StaticPageResourceEdit data);
    Task<PaginationList<StaticPageResourceRead>> GetAllStaticPageResource(int page, int cant, string filter);
    Task<StaticPageResourceRead> GetStaticPageResource(Guid id);
    Task<bool> DeleteStaticPageResource(Guid id);

    Task<Guid> CreateStaticPageDataTemplate(StaticPageDataTemplateCreate data);
    Task<Guid> EditStaticPageDataTemplate(StaticPageDataTemplateEdit data);
    Task<PaginationList<StaticPageDataTemplateRead>> GetAllStaticPageDataTemplate(int page, int cant, string filter);
    Task<StaticPageDataTemplateRead> GetStaticPageDataTemplate(Guid id);
    Task<bool> DeleteStaticPageDataTemplate(Guid id);

    Task<Guid> CreateStaticPageWebsiteTemplate(StaticPageWebsiteTemplateCreate data);
    Task<Guid> EditStaticPageWebsiteTemplate(StaticPageWebsiteTemplateEdit data);
    Task<PaginationList<StaticPageWebsiteTemplateRead>> GetAllStaticPageWebsiteTemplate(int page, int cant, string filter);
    Task<StaticPageWebsiteTemplateRead> GetStaticPageWebsiteTemplate(Guid id);
    Task<bool> DeleteStaticPageWebsiteTemplate(Guid id);

    Task<Guid> CreateStaticPageProduct(StaticPageProductCreate data);
    Task<Guid> EditStaticPageProduct(StaticPageProductEdit data);
    Task<PaginationList<StaticPageProductRead>> GetAllStaticPageProduct(int page, int cant, string filter);
    Task<StaticPageProductRead> GetStaticPageProduct(Guid id);
    Task<bool> DeleteStaticPageProduct(Guid id);
    Task AddResource(Guid productId, Guid resourceId);
    Task RemoveResource(Guid productId, Guid resourceId);

    Task<Guid> CreateStaticPageProductByPage(StaticPageProductByPageCreate data);
    Task<bool> ExitsStaticPageProductByPage(StaticPageProductByPageCreate data);
    Task<Guid> CreateStaticPageProductByResource(StaticPageProductByResourceCreate data);
    Task<bool> ExitsStaticPageProductByResource(StaticPageProductByResourceCreate data);
    Task<bool> DeleteStaticPageProductByPage(Guid id);
    Task<bool> DeleteStaticPageProductByResource(Guid id);

}
