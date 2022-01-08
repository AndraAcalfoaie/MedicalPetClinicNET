using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Models.Appointment;
using Services.Models.Doctor;
using System.Collections.Generic;
using WebAPI.Filters;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IProcedureService _procedureService;
        private readonly IAppointmentService _appointmentService;

        public DoctorController(IDoctorService doctorService, IProcedureService procedureService, IAppointmentService appointmentService)
        {
            _doctorService = doctorService;
            _procedureService = procedureService;
            _appointmentService = appointmentService;
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

        [HttpGet("{doctorId}/Schedule")]
        public DoctorSchedule GetDoctorSchedule([FromRoute] int doctorId)
        {
            return _appointmentService.GetDoctorSchedule(doctorId);
        }

        [HttpGet("{doctorId}/Appointments")]
        public List<AppointmentDto> GetDoctorAppointments([FromRoute] int doctorId)
        {
            var userId = HttpContext.GetUserId();
            return _appointmentService.GetByDoctorId(userId, doctorId);
        }
    }
}
