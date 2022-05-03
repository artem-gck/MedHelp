using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Services.Model
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string? CommentText { get; set; }
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
