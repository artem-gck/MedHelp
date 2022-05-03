using MedHelp.Client.Models;

namespace MedHelp.Client.Services
{
    public interface IPatientService
    {
        public Task<Patient> GetPatient(int id);
        public Task<List<Patient>> GetPatients();
    }
}
