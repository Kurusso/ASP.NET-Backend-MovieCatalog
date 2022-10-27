using System.Security.Claims;

namespace MovieCatalogBackend.Services
{
    public interface IUserIdentityService
    {
        public ClaimsIdentity GetIdentity(string username, string password);
        public Task AddJwtInBlackList(string jwt);
        public Task<bool> CheckJwtIsInBlackList(string jwt);
    }
}
