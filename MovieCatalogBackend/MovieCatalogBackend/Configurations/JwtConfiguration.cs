using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MovieCatalogBackend.Configurations
{
    public class JwtConfiguration
    {
        public const string Issuer = "JwtIssuer";
        public const string Audience = "JwtClient";
        public const string Key = "SuperSecretKeyBazingaLolKek!*228322";
        public const int Lifetime = 60;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }

    }
}
