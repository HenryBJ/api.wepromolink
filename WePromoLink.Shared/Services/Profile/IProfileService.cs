namespace WePromoLink.Services.Profile;

public interface IProfileService 
{
    Task<string> GetExternalId(string firebaseId);
}