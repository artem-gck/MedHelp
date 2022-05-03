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
    }
}
