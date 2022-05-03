using MedHelp.Client.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace MedHelp.Client.Services.Logic
{
    public class DoctorService : IDoctorService
    {
        private readonly HttpClient _httpClient;

        public DoctorService(HttpClient httpClient)
            => _httpClient = httpClient;

        public async Task<int> AddDoctor(Doctor doctor)
        {
            var resp = await _httpClient.PostAsJsonAsync("doctor", doctor);
            var idString = await resp.Content.ReadAsStringAsync();
            var id = int.Parse(idString);

            return id;
        }

        public async Task<int> DeleteDoctor(int id)
        {
            var resp = await _httpClient.DeleteAsync($"doctor/{id}");
            var idDString = await resp.Content.ReadAsStringAsync();
            var idD = int.Parse(idDString);

            return idD;
        }

        public async Task<List<Doctor>> GetDoctors()
        {
            var resp = await _httpClient.GetAsync("doctor");
            var doctorsString = await resp.Content.ReadAsStringAsync();
            var doctors = JsonConvert.DeserializeObject<List<Doctor>>(doctorsString);

            return doctors;
        }

        public async Task<int> UpdateDoctor(Doctor doctor)
        {
            var resp = await _httpClient.PutAsJsonAsync("doctor", doctor);
            var idString = await resp.Content.ReadAsStringAsync();
            var id = int.Parse(idString);

            return id;
        }

        public async Task<List<Tolon>> GetTolones(int doctorId)
        {
            var resp = await _httpClient.GetAsync($"doctor/tolon/{doctorId}");
            var tolonesString = await resp.Content.ReadAsStringAsync();
            var tolones = JsonConvert.DeserializeObject<List<Tolon>>(tolonesString);

            return tolones;
        }

        public async Task<int> AddTolon(Tolon tolon)
        {
            var resp = await _httpClient.PostAsJsonAsync("doctor/tolon", tolon);
            var idString = await resp.Content.ReadAsStringAsync();
            var id = int.Parse(idString);

            return id;
        }

        public async Task<int> DeleteTolon(int id)
        {
            var resp = await _httpClient.DeleteAsync($"doctor/tolon/{id}");
            var idDString = await resp.Content.ReadAsStringAsync();
            var idD = int.Parse(idDString);

            return idD;
        }

        public async Task<Tolon> GetTolon(int id)
        {
            var resp = await _httpClient.GetAsync($"doctor/tolon/tolonId/{id}");
            var tolonString = await resp.Content.ReadAsStringAsync();
            var tolon = JsonConvert.DeserializeObject<Tolon>(tolonString);

            return tolon;
        }

        public async Task<int> AddReception(Reception reception)
        {
            var resp = await _httpClient.PostAsJsonAsync("doctor/reception", reception);
            var idString = await resp.Content.ReadAsStringAsync();
            var id = int.Parse(idString);

            return id;
        }
    }
}
