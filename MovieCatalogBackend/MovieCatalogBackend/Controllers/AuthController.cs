using Microsoft.AspNetCore.Mvc;
using MovieCatalogBackend.Models;
using MovieCatalogBackend.Services;

namespace MovieCatalogBackend.Controllers
{
    [ApiController]
    [Route("api/account/")]
    public class AuthController:ControllerBase
    {
        private IUserAddService _userAddService;
        public AuthController(IUserAddService addUser)
        {
            _userAddService = addUser;
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
                return Ok(model);
            }
            catch(ArgumentException e)
            {
                return Problem(e.Message,statusCode:409);
            }
        }
    }
}
