using Microsoft.IdentityModel.JsonWebTokens;
using MovieCatalogBackend.Context;
using MovieCatalogBackend.Models;
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

        public async Task AddJwtInBlackList(string jwt)
        {
            _context.Jwt.Add(new JwtDbModel { Jwt = jwt });
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckJwtIsInBlackList(string jwt)
        {
            var Jwt = _context.Jwt.FirstOrDefault(x => x.Jwt == jwt);
            if(Jwt == null)
            {
                return false;
            }
            return true;
        }

        public ClaimsIdentity GetIdentity(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == username);
            if(user == null)
            {
                return null;
            }
            bool correctPasswor = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (user == null || !correctPasswor)
            {
                return null;
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim("IdClaim", user.Id.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Token",ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
        
    }
}
