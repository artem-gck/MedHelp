using MedHelp.Client.Models;

namespace MedHelp.Client.Services
{
    public interface IDoctorService
    {
        public Task<List<Doctor>> GetDoctors();
        public Task<int> AddDoctor(Doctor doctor);
        public Task<int> UpdateDoctor(Doctor doctor);
        public Task<int> DeleteDoctor(int id);
        public Task<List<Tolon>> GetTolones(int doctorId);
        public Task<int> AddTolon(Tolon tolon);
        public Task<int> DeleteTolon(int id);
        public Task<Tolon> GetTolon(int id);
        public Task<int> AddReception(Reception reception); 
    }
}
