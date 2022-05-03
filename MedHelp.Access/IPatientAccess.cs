using MedHelp.Access.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Access
{
    public interface IPatientAccess
    {
        public Task<Patient> GetPatient(int id);
        public Task<List<Patient>> GetPatients();
        public Task<List<Tolon>> GetTolones(int patientId);
        public Task<List<Reception>> GetReceptions(int idPatient);
        public Task<int> UpdatePatient(Patient patient);
        public Task<int> DeletePatient(int id);
        public Task<int> AddPatient(Patient patient);
        public Task<List<Patient>> Search(string searchString);
    }
}
