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

        public async Task<List<Tolon>> GetTolones(int patientId)
        {
            var tolones = await _patientAccess.GetTolones(patientId);

            return tolones.Select(t => MapTolonToMod(t)).ToList();
        }

        public async Task<List<Reception>> GetReception(int patientId)
        {
            var receptions = await _patientAccess.GetReceptions(patientId);

            return receptions.Select(rec => MapReceptionToMod(rec)).ToList();
        }

        public async Task<int> UpdatePatient(Patient patient)
        {
            return await _patientAccess.UpdatePatient(MapDoctorToEnt(patient));
        }

        public async Task<int> DeletePatient(int id)
        {
            return await _patientAccess.DeletePatient(id);
        }

        public async Task<int> AddPatient(Patient patient)
        {
            return await _patientAccess.AddPatient(MapDoctorToEnt(patient));
        }

        public async Task<List<Patient>> Search(string searchString)
        {
            var patients = await _patientAccess.Search(searchString);

            return patients.Select(pat => MapDoctorToMod(pat)).ToList();
        }

        private Reception MapReceptionToMod(Access.Entity.Reception reception)
        {
            var patient = new Patient()
            {
                PatientId = reception.Tolon.Patient.PatientId,
                Name = reception.Tolon.Patient.Name,
                FirstName = reception.Tolon.Patient.FirstName,
                LastName = reception.Tolon.Patient.LastName,
                NumberOfPhone = reception.Tolon.Patient.NumberOfPhone,
                DateOfDirth = reception.Tolon.Patient.DateOfDirth,
            };

            var doctor = new Doctor()
            {
                DoctorId = reception.Tolon.Doctor.DoctorId,
                Name = reception.Tolon.Doctor.Name,
                FirstName = reception.Tolon.Doctor.FirstName,
                LastName = reception.Tolon.Doctor.LastName,
                NumberOfPhone = reception.Tolon.Doctor.NumberOfPhone,
                Specialization = reception.Tolon.Doctor.Specialization,
            };

            var tolon = new Tolon()
            {
                TolonId = reception.Tolon.TolonId,
                Patient = patient,
                Doctor = doctor,
                Time = reception.Tolon.Time,
            };

            var disease = new Disease()
            {
                DiseaseId = reception.Disease.DiseaseId,
                Name = reception.Disease.Name,
                Description = reception.Disease.Description,
                Conclusion = reception.Disease.Conclusion,
            };

            var receptionEnt = new Reception()
            {
                Tolon = tolon,
                Disease = disease,
            };

            return receptionEnt;
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

        private Tolon MapTolonToMod(Access.Entity.Tolon tolon)
        {
            var doctorMod = new Doctor()
            {
                DoctorId = tolon.Doctor.DoctorId,
                Name = tolon.Doctor.Name,
                FirstName = tolon.Doctor.FirstName,
                LastName = tolon.Doctor.LastName,
                NumberOfPhone = tolon.Doctor.NumberOfPhone,
                Specialization = tolon.Doctor.Specialization,
            };

            var patientMod = new Patient()
            {
                PatientId = tolon.Patient.PatientId,
                Name = tolon.Patient.Name,
                FirstName = tolon.Patient.FirstName,
                LastName = tolon.Patient.LastName,
                NumberOfPhone = tolon.Patient.NumberOfPhone,
                DateOfDirth = tolon.Patient.DateOfDirth,
            };

            var tolonMod = new Tolon()
            {
                TolonId = tolon.TolonId,
                Patient = patientMod,
                Doctor = doctorMod,
                Time = tolon.Time,
            };

            return tolonMod;
        }
    }
}
