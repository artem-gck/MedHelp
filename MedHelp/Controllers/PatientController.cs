
using MedHelp.Services;
using MedHelp.Services.Model;
using Microsoft.AspNetCore.Mvc;

namespace MedHelp.Controllers
{
    [ApiController]
    [Route("api/patient")]
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
            => _patientService = patientService;

        [HttpGet]
        public async Task<ActionResult<List<Patient>>> GetPatients()
        {
            return await _patientService.GetPatients();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            return await _patientService.GetPatient(id);
        }

        [HttpGet("tolon/{id}")]
        public async Task<ActionResult<List<Tolon>>> GetTolones(int id)
        {
            return await _patientService.GetTolones(id);
        }

        [HttpGet("reception/{id}")]
        public async Task<ActionResult<List<Reception>>> GetReceptions(int id)
        {
            return await _patientService.GetReception(id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeletePatient(int id)
        {
            return await _patientService.DeletePatient(id);
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddPatient(Patient patient)
        {
            return await _patientService.AddPatient(patient);
        }

        [HttpPut]
        public async Task<ActionResult<int>> UpdatePatient(Patient patient)
        {
            return await _patientService.UpdatePatient(patient);
        }
    }
}
