using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Access.Entity
{
    public class Reception
    {
        public int ReceptionId { get; set; }
        public Tolon? Tolon { get; set; }
        public Disease? Disease { get; set; }
    }
}
