using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCatalogBackend.Models.DTO;
using MovieCatalogBackend.Services;

namespace MovieCatalogBackend.Controllers
{
    [Route("api/account/profile")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserProfileService _userProfileService;
        private IUserIdentityService _userIdentityService;

        public UserController(IUserProfileService userProfileService, IUserIdentityService userIdentityService)
        {
            _userProfileService = userProfileService;
            _userIdentityService = userIdentityService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ProfileModel>> Get()
        {
            string token = Request.Headers["Authorization"];
            bool check = await _userIdentityService.CheckJwtIsInBlackList(token);
            if (!check)
            {
                try
                {
                    var profile = await _userProfileService.GetUserProfile(User.FindFirst("IdClaim").Value);
                    return Ok(profile);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return Unauthorized("Jwt is in blacklist!");
      
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task <IActionResult> Put(ProfileModel model)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            string token = Request.Headers["Authorization"];
            bool check = await _userIdentityService.CheckJwtIsInBlackList(token);
            if (!check)
            {
                try
                {
                    await _userProfileService.ChangeUserProfile(model, User.FindFirst("IdClaim").Value);
                    return Ok();
                }
                catch (Exception e)
                {
                    return Problem(e.Message, statusCode:409);
                }
            }
            return Unauthorized("Jwt is in blacklist!");           
        }
    }
}
