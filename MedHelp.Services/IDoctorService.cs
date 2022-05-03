using MedHelp.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Services
{
    public interface IDoctorService
    {
        public Task<List<Doctor>> GetDoctors();
        public Task<int> UpdateDoctor(Doctor doctor);
        public Task<int> DeleteDoctor(int id);
        public Task<int> AddDoctor(Doctor doctor);
        public Task<List<Tolon>> GetTolones(int doctorId);
        public Task<int> AddTolon(Tolon tolon);
        public Task<int> DeleteTolon(int id);
        public Task<Tolon> GetTolon(int id);
        public Task<int> AddReception(Reception reception);
        public Task<List<Reception>> GetReception(int doctorId);
    }
}
