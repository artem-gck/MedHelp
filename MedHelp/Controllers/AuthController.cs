using MedHelp.Services;
using MedHelp.Services.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace MedHelp.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<ActionResult<bool>> Auth()
        {
            var user = new LoginUser()
            {
                Login = "a",
                Password = "q"
            };

            return await _authService.LoginAsync(user);
        }
    }
}
