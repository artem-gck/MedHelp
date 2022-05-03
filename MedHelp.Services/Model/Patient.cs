using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Services.Model
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string? Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NumberOfPhone { get; set; }
        public DateTime? DateOfDirth { get; set; }
        public Sex? Sex { get; set; }
        public List<Tolon>? Tolons { get; set; }
        public List<Comment>? Comments { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
