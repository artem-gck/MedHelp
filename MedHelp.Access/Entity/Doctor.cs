using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Access.Entity
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string? Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NumberOfPhone { get; set; }
        public string? Specialization { get; set; }
        public List<Tolon>? Tolons { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }  
    }
}
