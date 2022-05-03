using MedHelp.Access;
using MedHelp.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Services.Logic
{
    public class CommentService : ICommentService
    {
        private readonly ICommentAccess _commentAccess;

        public CommentService(ICommentAccess commentAccess)
        {
            _commentAccess = commentAccess;
        }

        public async Task<int> AddComment(Comment comment)
        {
            return await _commentAccess.AddComment(MapCommentToEnt(comment));
        }

        public async Task<List<Comment>> GetCommentsByDoctor(int doctorId)
        {
            var comments = await _commentAccess.GetCommentsByDoctor(doctorId);
            return comments.Select(com => MapCommentToMod(com)).ToList();
        }

        public async Task<List<Comment>> GetCommentsByPatient(int patientId)
        {
            var comments = await _commentAccess.GetCommentsByPatient(patientId);
            return comments.Select(com => MapCommentToMod(com)).ToList();
        }

        private Access.Entity.Comment MapCommentToEnt(Comment comment)
        {
            var doctor = new Access.Entity.Doctor()
            {
                DoctorId = comment.Doctor.DoctorId
            };

            var patient = new Access.Entity.Patient()
            {
                PatientId = comment.Patient.PatientId
            };

            var commentDb = new Access.Entity.Comment()
            {
                CommentText = comment.CommentText,
                Doctor = doctor,
                Patient = patient
            };

            return commentDb;
        }

        private Comment MapCommentToMod(Access.Entity.Comment comment)
        {
            var doctor = new Doctor()
            {
                DoctorId = comment.Doctor.DoctorId,
                Name = comment.Doctor.Name,
                FirstName = comment.Doctor.FirstName,
                LastName = comment.Doctor.LastName,
            };

            var patient = new Patient()
            {
                PatientId = comment.Patient.PatientId,
                Name = comment.Patient.Name,
                FirstName = comment.Patient.FirstName,
                LastName = comment.Patient.LastName,
            };

            var commentDb = new Comment()
            {
                CommentText = comment.CommentText,
                Doctor = doctor,
                Patient = patient
            };

            return commentDb;
        }
    }
}
