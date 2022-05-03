using MedHelp.Services;
using MedHelp.Services.Model;
using Microsoft.AspNetCore.Mvc;

namespace MedHelp.Controllers
{
    [ApiController]
    [Route("api/doctor")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
            => _doctorService = doctorService;

        [HttpGet]
        public async Task<ActionResult<List<Doctor>>> GetDoctors()
        {
            return await _doctorService.GetDoctors();
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddDoctor(Doctor doctor)
        {
            return await _doctorService.AddDoctor(doctor);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteDoctor(int id)
        {
            return await _doctorService.DeleteDoctor(id);
        }

        [HttpPut]
        public async Task<ActionResult<int>> UpdateDoctor(Doctor doctor)
        {
            return await _doctorService.UpdateDoctor(doctor);
        }

        [HttpGet("tolon/{id}")]
        public async Task<ActionResult<List<Tolon>>> GetTolones(int id)
        {
            return await _doctorService.GetTolones(id);
        }

        [HttpPost("tolon")]
        public async Task<ActionResult<int>> AddTolon(Tolon tolon)
        {
            return await _doctorService.AddTolon(tolon);
        }

        [HttpDelete("tolon/{id}")]
        public async Task<ActionResult<int>> DeleteTolon(int id)
        {
            return await _doctorService.DeleteTolon(id);
        }

        [HttpGet("tolon/tolonId/{id}")]
        public async Task<ActionResult<Tolon>> GetTolon(int id)
        {
            return await _doctorService.GetTolon(id);
        }

        [HttpPost("reception")]
        public async Task<ActionResult<int>> AddReception(Reception reception)
        {
            return await _doctorService.AddReception(reception);
        }
    }
}
