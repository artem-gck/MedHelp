using MedHelp.Client.Models;
using Newtonsoft.Json;

namespace MedHelp.Client.Services.Logic
{
    public class PatientService : IPatientService
    {
        private readonly HttpClient _httpClient;

        public PatientService(HttpClient httpClient)
            => _httpClient = httpClient;

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
    }
}
