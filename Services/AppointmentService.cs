using AutoMapper;
using DataAccess;
using DataAccess.Models;
using Services.Exceptions;
using Services.Models.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public interface IAppointmentService
    {
        List<Appointment> GetByDoctorId(int doctorId);
        List<Appointment> GetByPatientId(int userId, int patientId);
        int AddAppointment(CreateAppointmentDto appointment);
        void DeleteAppointment(int userId, int appointmentId);
    }

    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public AppointmentService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int AddAppointment(CreateAppointmentDto appointment)
        {
            var dbAppointment = _mapper.Map<Appointment>(appointment);
            _dbContext.Add(dbAppointment);
            _dbContext.SaveChanges();

            return dbAppointment.Id;
        }

        public void DeleteAppointment(int userId, int appointmentId)
        {
            var appointment = _dbContext.Appointments.Find(appointmentId);
            if (appointment.Patient.UserId != userId) throw new UnauthorizedException();

            _dbContext.Appointments.Remove(appointment);
            _dbContext.SaveChanges();
        }

        public List<Appointment> GetByDoctorId(int doctorId)
        {
            return _dbContext.Appointments.Where(x => x.DoctorId == doctorId).ToList();
        }

        public List<Appointment> GetByPatientId(int userId, int patientId)
        {
            var patient = _dbContext.Patients.Find(patientId);
            if (patient.UserId != userId) throw new UnauthorizedAccessException();

            return patient.Appointments.ToList();
        }
    }
}
