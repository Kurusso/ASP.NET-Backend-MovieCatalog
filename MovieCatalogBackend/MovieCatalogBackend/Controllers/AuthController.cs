using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieCatalogBackend.Configurations;
using MovieCatalogBackend.Models;
using MovieCatalogBackend.Models.DTO;
using MovieCatalogBackend.Services;
using System.IdentityModel.Tokens.Jwt;

namespace MovieCatalogBackend.Controllers
{
    [ApiController]
    [Route("api/account/")]
    public class AuthController:ControllerBase
    {
        private IUserAddService _userAddService;
        private IUserIdentityService _userIdentityService;
        public AuthController(IUserAddService addUser,IUserIdentityService identityUser)
        {
            _userAddService = addUser;
            _userIdentityService = identityUser;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
               await _userAddService.AddUser(model);
                var identity = _userIdentityService.GetIdentity(model.UserName, model.Password);
                if (identity == null)
                {
                    return BadRequest(new { errorText = "Invalid username or password." });
                }
                var now = DateTime.UtcNow;
                var jwt = new JwtSecurityToken(
                    issuer: JwtConfiguration.Issuer,
                    audience: JwtConfiguration.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(new TimeSpan(0, 0, JwtConfiguration.Lifetime, 0)),
                    signingCredentials: new SigningCredentials(JwtConfiguration.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                var response = new
                {
                    token = encodedJwt,
                };
                return new JsonResult(response);
            }
            catch(ArgumentException e)
            {
                return Problem(e.Message,statusCode:409);
            }
        }
        [HttpPost("login")]
        public IActionResult Token([FromBody] LoginCredentials model)
        {
            var identity =  _userIdentityService.GetIdentity(model.UserName,model.Password);
            if(identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }
            var now = DateTime.UtcNow;
            var jwt= new JwtSecurityToken(
                issuer:JwtConfiguration.Issuer,
                audience:JwtConfiguration.Audience,
                notBefore:now,
                claims:identity.Claims,
                expires: now.Add(new TimeSpan(0,0,JwtConfiguration.Lifetime,0)),
                signingCredentials: new SigningCredentials(JwtConfiguration.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt= new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                token = encodedJwt,
            };
            return new JsonResult(response);
                
        }
        [HttpPost("logout")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Logout()
        {
            
            string token = Request.Headers["Authorization"];
            _userIdentityService.AddJwtInBlackList(token);
            var response = new
            {
                token = "",
                message = "Logged Out"
            };
            return new JsonResult(response);
        }

    }
}
