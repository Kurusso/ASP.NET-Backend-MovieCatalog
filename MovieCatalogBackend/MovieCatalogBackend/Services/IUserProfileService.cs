using Microsoft.AspNetCore.Mvc;
using MovieCatalogBackend.Models.DTO;

namespace MovieCatalogBackend.Services
{
    public interface IUserProfileService
    {
        public Task<ProfileModel> GetUserProfile(string id);
        public Task ChangeUserProfile (ProfileModel model, string id);
    }
}
