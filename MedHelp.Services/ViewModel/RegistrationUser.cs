using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Services.ViewModel
{
    public class RegistrationUser
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string? Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NumberOfPhone { get; set; }
        public DateTime? DateOfDirth { get; set; }
        public string? Sex { get; set; }
    }
}
