using MedHelp.Client.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace MedHelp.Client.Services.Logic
{
    public class PatientService : IPatientService
    {
        private readonly HttpClient _httpClient;

        public PatientService(HttpClient httpClient)
            => _httpClient = httpClient;

        public async Task<int> AddPatient(Patient patient)
        {
            var resp = await _httpClient.PostAsJsonAsync($"patient", patient);
            var idString = await resp.Content.ReadAsStringAsync();
            var id = int.Parse(idString);

            return id;
        }

        public async Task<int> DeletePatient(int id)
        {
            var resp = await _httpClient.DeleteAsync($"patient/{id}");
            var idPString = await resp.Content.ReadAsStringAsync();
            var idP = int.Parse(idPString);

            return idP;
        }

        public async Task<Patient> GetPatient(int id)
        {
            var resp = await _httpClient.GetAsync($"patient/{id}");
            var patientString = await resp.Content.ReadAsStringAsync();
            var patient = JsonConvert.DeserializeObject<Patient>(patientString);

            return patient;
        }

        public async Task<List<Patient>> GetPatients()
        {
            var resp = await _httpClient.GetAsync("patient");
            var patientsString = await resp.Content.ReadAsStringAsync();
            var patients = JsonConvert.DeserializeObject<List<Patient>>(patientsString);

            return patients;
        }

        public async Task<List<Reception>> GetReceptions(int patientId)
        {
            var resp = await _httpClient.GetAsync($"patient/reception/{patientId}");
            var receptionsString = await resp.Content.ReadAsStringAsync();
            var receptions = JsonConvert.DeserializeObject<List<Reception>>(receptionsString);

            return receptions;
        }

        public async Task<List<Tolon>> GetTolones(int patientId)
        {
            var resp = await _httpClient.GetAsync($"patient/tolon/{patientId}");
            var tolonesString = await resp.Content.ReadAsStringAsync();
            var tolones = JsonConvert.DeserializeObject<List<Tolon>>(tolonesString);

            return tolones;
        }

        public async Task<List<Patient>> Search(string search)
        {
            var resp = await _httpClient.GetAsync($"patient/search?search={search}");
            var searchString = await resp.Content.ReadAsStringAsync();
            var tolones = JsonConvert.DeserializeObject<List<Patient>>(searchString);

            return tolones;
        }

        public async Task<int> UpdatePatient(Patient patient)
        {
            var resp = await _httpClient.PutAsJsonAsync($"patient", patient);
            var idString = await resp.Content.ReadAsStringAsync();
            var id = int.Parse(idString);

            return id;
        }
    }
}
