using LAB_11.DTOs;
using LAB_11.Services;
using Microsoft.AspNetCore.Mvc;

namespace LAB_11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public PrescriptionsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPrescription(PrescriptionPatientMedicamentDoctorDTO prescription)
        {
            try
            {
                var resultID = await _dbService.AddPrescription(prescription);
                return Ok("Prescription added on id: " + resultID);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            } 
        }
    }
}
