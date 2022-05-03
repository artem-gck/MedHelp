namespace MedHelp.Client.Models
{
    public class PatientView
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NumberOfPhone { get; set; }
        public DateTime DateOfDirth { get; set; }
        public string Sex { get; set; }
        public int Tolons { get; set; }
    }
}
