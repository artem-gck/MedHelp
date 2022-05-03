using MedHelp.Client.Models;

namespace MedHelp.Client.Services
{
    public interface IAuthService
    {
        public Task<Tokens> Login(LoginModel loginModel);
        public Task<int> Registration(RegistrationUser registrationModel);
        public Task<User> GetUser(string login);
    }
}
