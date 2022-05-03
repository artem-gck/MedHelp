using MedHelp.Services;
using MedHelp.Services.Model;
using MedHelp.Services.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace MedHelp.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly string _headerName;

        public AuthController(IAuthService authService, ITokenService tokenService, IConfiguration configuration)
            => (_authService, _tokenService, _headerName) = (authService, tokenService, configuration.GetSection("HeaderName").Value);

        [HttpPost("login")]
        public async Task<ActionResult<Tokens>> Login([FromBody] LoginUser loginModel)
        {
            var user = await _authService.GetUser(loginModel.Login);

            if (user is null)
                return null;

            var tokens = await _authService.LoginAsync(user);

            if (tokens is null)
                return null;

            tokens.Login = loginModel.Login;
            tokens.Role = user.Doctor is not null ? "doctor" : "patient";

            if (tokens is null)
                return Unauthorized();

            return tokens;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsersAsync()
        {
            //if (await _tokenService.CheckAccessKey(Request.Headers[_headerName].ToString()))
                return await _authService.GetUsersAsync();
            //else
            //    return Unauthorized();
        }

        [HttpPost]
        public async Task<ActionResult<int>> RegistrationAsync(RegistrationUser userView)
        {
            var sex = new Sex()
            {
                Value = userView.Sex
            };

            var patient = new Patient()
            {
                Name = userView.Name,
                FirstName = userView.FirstName,
                LastName = userView.LastName,
                NumberOfPhone = userView.NumberOfPhone,
                DateOfDirth = userView.DateOfDirth,
                Sex = sex
            };

            var user = new User()
            {
                Login = userView.Login,
                Password = userView.Password,
                Patient = patient,
            };

            return await _authService.RegistrationAsync(user);
        }

        [HttpPut]
        public async Task<ActionResult<int>> UpdateUserAsync([FromBody] User user)
        {

                return await _authService.UpdateUserAsync(user);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteUserAsync(int id)
        {

                return await _authService.DeleteUserAsync(id);

        }

        [HttpGet("user")]
        public async Task<ActionResult<User>> GetUser(string login)
        {

            return await _authService.GetUser(login);

        }

        [HttpGet("sexes")]
        public async Task<ActionResult<List<Sex>>> GetAllSexes()
        {
            return await _authService.GetAllSexes();
        }

        [HttpPost("add")]
        public async Task<ActionResult<int>> AddUserAsync([FromBody] User user)
        {

                return await _authService.AddUserAsync(user);

        }

        [HttpGet("search/{search}")]
        public async Task<ActionResult<List<User>>> SearchUsersAsync(string search)
        {

                return await _authService.SearchUsersAsync(search);

        }
    }
}
