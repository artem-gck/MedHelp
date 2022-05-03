using MedHelp.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Services
{
    public interface IPatientService
    {
        public Task<Patient> GetPatient(int id);
        public Task<List<Patient>> GetPatients();
        public Task<List<Tolon>> GetTolones(int patientId);
        public Task<List<Reception>> GetReception(int patientId);
        public Task<int> UpdatePatient(Patient patient);
        public Task<int> DeletePatient(int id);
        public Task<int> AddPatient(Patient patient);
        public Task<List<Patient>> Search(string searchString);
    }
}
