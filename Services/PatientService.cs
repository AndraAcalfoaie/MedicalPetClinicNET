using AutoMapper;
using DataAccess;
using DataAccess.Models;
using Services.Exceptions;
using Services.Models.Patient;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public interface IPatientService
    {
        List<PatientDto> GetAllByUserId(int userId);
        int AddPatient(int userId, EditPatientDto patient);
        void UpdatePatient(int userId, int patientId, EditPatientDto patient);
        void DeletePatient(int userId, int patientId);
    }

    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public PatientService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<PatientDto> GetAllByUserId(int userId)
        {
            var dbPatients = _dbContext.Patients.Where(p => p.UserId == userId).ToList();
            return _mapper.Map<List<PatientDto>>(dbPatients);
        }

        public int AddPatient(int userId, EditPatientDto patient)
        {
            var dbPatient = _mapper.Map<Patient>(patient);
            dbPatient.UserId = userId;

            _dbContext.Patients.Add(dbPatient);
            _dbContext.SaveChanges();

            return dbPatient.Id;
        }

        public void DeletePatient(int userId, int patientId)
        {
            var dbPatient = _dbContext.Patients.First(x => x.Id == patientId && x.UserId == userId);
            _dbContext.Patients.Remove(dbPatient);
            _dbContext.SaveChanges();
        }

        public void UpdatePatient(int userId, int patientId, EditPatientDto patient)
        {
            var dbPatient = _dbContext.Patients.Find(patientId);

            if (dbPatient.UserId != userId) throw new UnauthorizedException();

            _mapper.Map(patient, dbPatient);
            _dbContext.SaveChanges();
        }
    }
}
