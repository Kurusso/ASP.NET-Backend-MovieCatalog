using MovieCatalogBackend.Context;
using System.Security.Claims;

namespace MovieCatalogBackend.Services
{
    public class UserIdentityService : IUserIdentityService
    {
        private MovieCatalogDbContext _context;
        public UserIdentityService(MovieCatalogDbContext context)
        {
            _context = context;
        }
        public ClaimsIdentity GetIdentity(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == username);
            bool correctPasswor = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (user == null || !correctPasswor)
            {
                return null;
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Token",ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
