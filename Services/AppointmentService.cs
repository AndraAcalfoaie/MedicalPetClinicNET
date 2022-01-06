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
        List<Appointment> GetByPatientId(int clientId, int patientId);
        void AddAppointment(CreateAppointmentDto appointment);
        void DeleteAppointment(int clientId, int appointmentId);
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

        public void AddAppointment(CreateAppointmentDto appointment)
        {
            var dbAppointment = _mapper.Map<Appointment>(appointment);
            _dbContext.Add(dbAppointment);
            _dbContext.SaveChanges();
        }

        public void DeleteAppointment(int clientId, int appointmentId)
        {
            var appointment = _dbContext.Appointments.Find(appointmentId);
            if (appointment.Patient.ClientId != clientId) throw new UnauthorizedException();

            _dbContext.Appointments.Remove(appointment);
            _dbContext.SaveChanges();
        }

        public List<Appointment> GetByDoctorId(int doctorId)
        {
            return _dbContext.Appointments.Where(x => x.DoctorId == doctorId).ToList();
        }

        public List<Appointment> GetByPatientId(int clientId, int patientId)
        {
            var patient = _dbContext.Patients.Find(patientId);
            if (patient.ClientId != clientId) throw new UnauthorizedAccessException();

            return patient.Appointments.ToList();
        }
    }
}
