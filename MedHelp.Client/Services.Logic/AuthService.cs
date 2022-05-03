using MedHelp.Client.Models;
using MedHelp.Client.Services;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MedHelp.Client.Services.Logic
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
            => _httpClient = httpClient;

        public async Task<User> GetUser(string login)
        {
            var resp = await _httpClient.GetAsync($"auth/user?login={login}");
            var userString = await resp.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(userString);

            return user;
        }

        public async Task<Tokens> Login(LoginModel loginModel)
        {
            var resp = await _httpClient.PostAsJsonAsync("auth/login", loginModel);
            var tokensString = await resp.Content.ReadAsStringAsync();
            var tokens = JsonConvert.DeserializeObject<Tokens>(tokensString);

            return tokens;
        }

        public async Task<int> Registration(RegistrationUser registrationModel)
        {
            var resp = await _httpClient.PostAsJsonAsync("auth", registrationModel);
            var idString = await resp.Content.ReadAsStringAsync();
            var id = int.Parse(idString);

            return id;
        }
    }
}
