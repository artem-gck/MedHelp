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

        public async Task<List<Tolon>> GetTolones(int patientId)
        {
            return await _medHelpContext.Tolons.Include(t => t.Doctor)
                                               .Include(t => t.Patient)
                                               .ThenInclude(p => p.Sex)
                                               .Where(t => t.Patient.PatientId == patientId)
                                               .ToListAsync();
        }

        public async Task<List<Reception>> GetReceptions(int idPatient)
        {
            return await _medHelpContext.Receptions.Include(rec => rec.Disease)
                                                   .Include(rec => rec.Tolon)
                                                   .ThenInclude(tol => tol.Patient)
                                                   .Include(rec => rec.Tolon)
                                                   .ThenInclude(tol => tol.Doctor)
                                                   .Where(rec => rec.Tolon.Patient.PatientId == idPatient)
                                                   .ToListAsync();
        }

        public async Task<int> UpdatePatient(Patient patient)
        {
            var patientDb = await _medHelpContext.Patients.Include(pat => pat.User).Include(pat => pat.Sex).FirstOrDefaultAsync(pat => pat.PatientId == patient.PatientId);

            patientDb.User.Login = patient.User.Login;
            patientDb.User.Password = patient.User.Password;
            patientDb.Sex.Value = patient.Sex.Value;
            patientDb.DateOfDirth = patient.DateOfDirth;
            patientDb.Name = patient.Name;
            patientDb.FirstName = patient.FirstName;
            patientDb.LastName = patient.LastName;
            patientDb.NumberOfPhone = patient.NumberOfPhone;

            await _medHelpContext.SaveChangesAsync();

            return patientDb.PatientId;
        }

        public async Task<int> DeletePatient(int id)
        {
            var patientDb = await _medHelpContext.Patients.Include(pat => pat.User).FirstOrDefaultAsync(pat => pat.PatientId == id);

            var ent = _medHelpContext.Users.Remove(patientDb.User);

            await _medHelpContext.SaveChangesAsync();

            return ent.Entity.UserId;
        }

        public async Task<int> AddPatient(Patient patient)
        {
            var sex = await _medHelpContext.Sexes.FirstOrDefaultAsync(sex => sex.Value == patient.Sex.Value);
            patient.Sex = sex;
            var patientDb = await _medHelpContext.Patients.AddAsync(patient);

            await _medHelpContext.SaveChangesAsync();

            return patientDb.Entity.UserId;
        }

        public async Task<List<Patient>> Search(string searchString)
        {
            return await _medHelpContext.Patients.Include(pat => pat.User)
                                                 .Include(pat => pat.Sex)
                                                 .Include(pat => pat.Tolons)
                                                 .Where(pat => pat.User.Login.Contains(searchString) || 
                                                               pat.User.Password.Contains(searchString) || 
                                                               pat.Name.Contains(searchString) || 
                                                               pat.FirstName.Contains(searchString) || 
                                                               pat.LastName.Contains(searchString) || 
                                                               pat.DateOfDirth.ToString().Contains(searchString) || 
                                                               pat.NumberOfPhone.Contains(searchString))
                                                 .ToListAsync();
        }
    }
}
