using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Models.Patient;
using System.Collections.Generic;
using WebAPI.Filters;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [Authorize]
        [HttpGet]
        public List<PatientDto> GetAll()
        {
            var userId = HttpContext.GetUserId();
            return _patientService.GetAllByUserId(userId);
        }

        [Authorize]
        [HttpPost]
        public ActionResult<int> Add([FromBody] EditPatientDto patient)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = HttpContext.GetUserId();
            var pateitnId = _patientService.AddPatient(userId, patient);

            return pateitnId;
        }

        [Authorize]
        [HttpPut("{patientId}")]
        public IActionResult Update([FromRoute] int patientId, [FromBody] EditPatientDto patient)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = HttpContext.GetUserId();
            _patientService.UpdatePatient(userId, patientId, patient);

            return Ok();
        }

        [Authorize]
        [HttpDelete("{patientId}")]
        public IActionResult Delete([FromRoute] int patientId)
        {
            var userId = HttpContext.GetUserId();
            _patientService.DeletePatient(userId, patientId);

            return Ok();
        }
    }
}
