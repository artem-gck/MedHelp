using MedHelp.Client.Models;
using Newtonsoft.Json;

namespace MedHelp.Client.Services.Logic
{
    public class BasicService : IBasicService
    {
        private readonly HttpClient _httpClient;

        public BasicService(HttpClient httpClient)
            => _httpClient = httpClient;

        public async Task<List<Sex>> GetAllSexes()
        {
            var resp = await _httpClient.GetAsync("auth/sexes");
            var sexesString = await resp.Content.ReadAsStringAsync();
            var sexes = JsonConvert.DeserializeObject<List<Sex>>(sexesString);

            return sexes;
        }
    }
}
