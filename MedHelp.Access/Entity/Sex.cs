using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Access.Entity
{
    public class Sex
    {
        public int SexId { get; set; }
        public string Value { get; set; }
        public List<Patient>? Patients { get; set; }
    }
}
