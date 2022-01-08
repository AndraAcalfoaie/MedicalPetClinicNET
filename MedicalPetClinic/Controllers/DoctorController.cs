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

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
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
    }
}
