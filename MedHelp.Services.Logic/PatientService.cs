using MedHelp.Access;
using MedHelp.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Services.Logic
{
    public class PatientService : IPatientService
    {
        private readonly IPatientAccess _patientAccess;

        public PatientService(IPatientAccess patientAccess)
        {
            _patientAccess = patientAccess;
        }

        public async Task<Patient> GetPatient(int id)
        {
            var patient = await _patientAccess.GetPatient(id);
            return MapDoctorToMod(patient);
        }

        public async Task<List<Patient>> GetPatients()
        {
            var patients = await _patientAccess.GetPatients();

            return patients.Select(pa => MapDoctorToMod(pa)).ToList();
        }

        private Patient MapDoctorToMod(Access.Entity.Patient patient)
        {
            var sex = new Sex()
            {
                SexId = patient.Sex.SexId,
                Value = patient.Sex.Value
            };

            var tolons = patient.Tolons.Select(t => new Tolon()
            {
                TolonId = t.TolonId
            }).ToList();

            var user = new User()
            {
                UserId = patient.User.UserId,
                Login = patient.User.Login,
                Password = patient.User.Password,
            };

            var patientMod = new Patient()
            {
                PatientId = patient.PatientId,
                Name = patient.Name,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                NumberOfPhone = patient.NumberOfPhone,
                DateOfDirth = patient.DateOfDirth,
                Sex = sex,
                Tolons = tolons,
                User = user
            };

            return patientMod;
        }

        private Access.Entity.Patient MapDoctorToEnt(Patient patient)
        {
            var sex = new Access.Entity.Sex()
            {
                SexId = patient.Sex.SexId,
                Value = patient.Sex.Value
            };

            var tolons = patient.Tolons.Select(t => new Access.Entity.Tolon()
            {
                TolonId = t.TolonId
            }).ToList();

            var user = new Access.Entity.User()
            {
                UserId = patient.User.UserId,
                Login = patient.User.Login,
                Password = patient.User.Password,
            };

            var patientMod = new Access.Entity.Patient()
            {
                PatientId = patient.PatientId,
                Name = patient.Name,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                NumberOfPhone = patient.NumberOfPhone,
                DateOfDirth = patient.DateOfDirth,
                Sex = sex,
                Tolons = tolons,
                User = user
            };

            return patientMod;
        }
    }
}
