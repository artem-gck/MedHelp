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
    }
}
