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
    public class CommentAccess : ICommentAccess
    {
        private readonly MedHelpContext _medHelpContext;

        public CommentAccess(MedHelpContext medHelpContext)
        {
            _medHelpContext = medHelpContext;
        }

        public async Task<int> AddComment(Comment comment)
        {
            var doctor = await _medHelpContext.Doctors.FindAsync(comment.Doctor.DoctorId);
            var patient = await _medHelpContext.Patients.FindAsync(comment.Patient.PatientId);
            comment.Doctor = doctor;
            comment.Patient = patient;

            var commentDb = await _medHelpContext.Comments.AddAsync(comment);

            await _medHelpContext.SaveChangesAsync();

            return commentDb.Entity.CommentId;
        }

        public async Task<List<Comment>> GetCommentsByDoctor(int doctorId)
        {
            return await _medHelpContext.Comments.Include(com => com.Doctor)
                                                 .Include(com => com.Patient)
                                                 .Where(com => com.Doctor.DoctorId == doctorId)
                                                 .ToListAsync();
        }

        public async Task<List<Comment>> GetCommentsByPatient(int patientId)
        {
            return await _medHelpContext.Comments.Include(com => com.Doctor)
                                                 .Include(com => com.Patient)
                                                 .Where(com => com.Patient.PatientId == patientId)
                                                 .ToListAsync();
        }
    }
}
