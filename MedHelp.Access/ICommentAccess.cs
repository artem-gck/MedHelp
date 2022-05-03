using MedHelp.Access.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Access
{
    public interface ICommentAccess
    {
        public Task<int> AddComment(Comment comment);
        public Task<List<Comment>> GetCommentsByDoctor(int doctorId);
        public Task<List<Comment>> GetCommentsByPatient(int patientId);

    }
}
