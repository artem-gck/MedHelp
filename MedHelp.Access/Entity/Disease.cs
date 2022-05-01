using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Access.Entity
{
    public class Disease
    {
        public int DiseaseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Conclusion { get; set; }
        public int ReceptionId { get; set; }
        public Reception? Reception { get; set; }
    }
}
