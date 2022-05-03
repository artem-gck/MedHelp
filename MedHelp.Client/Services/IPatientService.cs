using MedHelp.Client.Models;

namespace MedHelp.Client.Services
{
    public interface IPatientService
    {
        public Task<Patient> GetPatient(int id);
        public Task<List<Patient>> GetPatients();
        public Task<List<Tolon>> GetTolones(int patientId);
        public Task<List<Reception>> GetReceptions(int patientId);
        public Task<int> UpdatePatient(Patient patient);
        public Task<int> DeletePatient(int id);
        public Task<int> AddPatient(Patient patient);
    }
}
