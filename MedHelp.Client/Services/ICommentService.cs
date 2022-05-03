using MedHelp.Client.Models;

namespace MedHelp.Client.Services
{
    public interface ICommentService
    {
        public Task<int> AddComment(Comment comment);
        public Task<List<Comment>> GetCommentsByDoctor(int doctorId);
        public Task<List<Comment>> GetCommentsByPatient(int patientId);
    }
}
