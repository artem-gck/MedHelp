﻿
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
    }
}