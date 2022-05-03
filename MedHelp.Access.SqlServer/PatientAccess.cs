using MedHelp.Access.Context;
using MedHelp.Access.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Access.SqlServer
{
    public class PatientAccess : IPatientAccess
    {
        private readonly MedHelpContext _medHelpContext;

        public PatientAccess(MedHelpContext medHelpContext)
        {
            _medHelpContext = medHelpContext;
        }

        public async Task<Patient> GetPatient(int id)
        {
            return await _medHelpContext.Patients.Include(pa => pa.Sex)
                                                 .Include(pa => pa.User)
                                                 .Include(pa => pa.Tolons)
                                                 .FirstOrDefaultAsync(pa => pa.PatientId == id);
        }

        public async Task<List<Patient>> GetPatients()
        {
            return await _medHelpContext.Patients.Include(pa => pa.Sex)
                                                 .Include(pa => pa.User)
                                                 .Include(pa => pa.Tolons)
                                                 .ToListAsync();
        }
    }
}
