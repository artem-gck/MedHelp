using MedHelp.Access;
using MedHelp.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Services.Logic
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorAccess _doctorAccess;

        public DoctorService(IDoctorAccess doctorAccess)
        {
            _doctorAccess = doctorAccess;
        }

        public async Task<List<Doctor>> GetDoctors()
        {
            var doctors = await _doctorAccess.GetDoctors();

            return doctors.Select(doc => MapDoctorToMod(doc)).ToList();
        }

        public async Task<int> UpdateDoctor(Doctor doctor)
        {
            return await _doctorAccess.UpdateDoctor(MapDoctorToEnt(doctor));
        }

        public async Task<int> DeleteDoctor(int id)
        {
            return await _doctorAccess.DeleteDoctor(id);
        }

        public Task<int> AddDoctor(Doctor doctor)
        {
            return _doctorAccess.AddDoctor(MapDoctorToEnt(doctor));
        }

        public async Task<List<Tolon>> GetTolones(int doctorId)
        {
            var tolones = await _doctorAccess.GetTolones(doctorId);

            return tolones.Select(t => MapTolonToMod(t)).ToList();
        }

        public async Task<int> AddTolon(Tolon tolon)
        {
            return await _doctorAccess.AddTolon(MapTolonToEnt(tolon));
        }

        public async Task<int> DeleteTolon(int id)
        {
            return await _doctorAccess.DeleteTolon(id);
        }

        public async Task<Tolon> GetTolon(int id)
        {
            var tolon = await _doctorAccess.GetTolon(id);
            return MapTolonToMod(tolon);
        }

        public async Task<int> AddReception(Reception reception)
        {
            return await _doctorAccess.AddReception(MapReceptionToEnt(reception));
        }

        public async Task<List<Reception>> GetReception(int doctorId)
        {
            var receptions = await _doctorAccess.GetReceptions(doctorId);

            return receptions.Select(rec => MapReceptionToMod(rec)).ToList();
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

        private Access.Entity.Reception MapReceptionToEnt(Reception reception)
        {
            var tolon = new Access.Entity.Tolon()
            {
                TolonId = reception.Tolon.TolonId
            };

            var disease = new Access.Entity.Disease()
            {
                DiseaseId = reception.Disease.DiseaseId,
                Name = reception.Disease.Name,
                Description = reception.Disease.Description,
                Conclusion = reception.Disease.Conclusion,
            };

            var receptionEnt = new Access.Entity.Reception()
            {
                Tolon = tolon,
                Disease = disease,
            };

            return receptionEnt;
        }

        private Doctor MapDoctorToMod(Access.Entity.Doctor doctor)
        {
            var tolons = doctor.Tolons.Select(t => new Tolon()
            {
                TolonId = t.TolonId
            }).ToList();

            var user = new User()
            {
                UserId = doctor.User.UserId,
                Login = doctor.User.Login,
                Password = doctor.User.Password,
            };

            var doctorMod = new Doctor()
            {
                DoctorId = doctor.DoctorId,
                Name = doctor.Name,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                NumberOfPhone = doctor.NumberOfPhone,
                Specialization = doctor.Specialization,
                Tolons = tolons,
                User = user
            };

            return doctorMod;
        }

        private Access.Entity.Doctor MapDoctorToEnt(Doctor doctor)
        {
            var tolons = doctor.Tolons.Select(t => new Access.Entity.Tolon()
            {
                TolonId = t.TolonId
            }).ToList();

            var user = new Access.Entity.User()
            {
                UserId = doctor.User.UserId,
                Login = doctor.User.Login,
                Password = doctor.User.Password,
            };

            var doctorEnt = new Access.Entity.Doctor()
            {
                DoctorId = doctor.DoctorId,
                Name = doctor.Name,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                NumberOfPhone = doctor.NumberOfPhone,
                Specialization = doctor.Specialization,
                Tolons = tolons,
                User = user
            };

            return doctorEnt;
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

        private Access.Entity.Tolon MapTolonToEnt(Tolon tolon)
        {
            var doctorMod = new Access.Entity.Doctor()
            {
                DoctorId = tolon.Doctor.DoctorId,
                Name = tolon.Doctor.Name,
                FirstName = tolon.Doctor.FirstName,
                LastName = tolon.Doctor.LastName,
                NumberOfPhone = tolon.Doctor.NumberOfPhone,
                Specialization = tolon.Doctor.Specialization,
            };

            var patientMod = new Access.Entity.Patient()
            {
                PatientId = tolon.Patient.PatientId,
                Name = tolon.Patient.Name,
                FirstName = tolon.Patient.FirstName,
                LastName = tolon.Patient.LastName,
                NumberOfPhone = tolon.Patient.NumberOfPhone,
                DateOfDirth = tolon.Patient.DateOfDirth
            };

            var tolonMod = new Access.Entity.Tolon()
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
