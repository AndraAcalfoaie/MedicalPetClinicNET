using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Models.Doctor;
using System.Collections.Generic;
using WebAPI.Filters;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IProcedureService _procedureService;

        public DoctorController(IDoctorService doctorService, IProcedureService procedureService)
        {
            _doctorService = doctorService;
            _procedureService = procedureService;
        }

        [HttpGet]
        public List<DoctorDto> GetAll()
        {
            return _doctorService.GetAll();
        }

        [Authorize(IsAdmin = true)]
        [HttpPost]
        public ActionResult<int> Add([FromBody] EditDoctorDto doctor)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var doctorId = _doctorService.Add(doctor);
            return doctorId;
        }

        [Authorize(IsAdmin = true)]
        [HttpPut("{doctorId}")]
        public IActionResult Update([FromRoute] int doctorId, [FromBody] EditDoctorDto doctor)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _doctorService.Update(doctorId, doctor);

            return Ok();
        }

        [Authorize(IsAdmin = true)]
        [HttpDelete("{doctorId}")]
        public IActionResult Delete([FromRoute] int doctorId)
        {
            _doctorService.Detele(doctorId);

            return Ok();
        }

        [HttpGet("Procedures")]
        public List<ProcedureDto> GetProcedures()
        {
            return _procedureService.GetAll();
        }
    }
}
