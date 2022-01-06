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
        List<PatientDto> GetAllByClientId(int clientId);
        void AddPatient(int clientId, EditPatientDto patient);
        void UpdatePatient(int clientId, int patientId, EditPatientDto patient);
        void DeletePatient(int clientId, int patientId);
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

        public List<PatientDto> GetAllByClientId(int clientId)
        {
            var dbPatients = _dbContext.Patients.Where(p => p.ClientId == clientId).ToList();
            return _mapper.Map<List<PatientDto>>(dbPatients);
        }

        public void AddPatient(int clientId, EditPatientDto patient)
        {
            var dbPatient = _mapper.Map<Patient>(patient);
            dbPatient.ClientId = clientId;

            _dbContext.Patients.Add(dbPatient);
            _dbContext.SaveChanges();
        }

        public void DeletePatient(int clientId, int patientId)
        {
            var dbPatient = _dbContext.Patients.First(x => x.Id == patientId && x.ClientId == clientId);
            _dbContext.Patients.Remove(dbPatient);
            _dbContext.SaveChanges();
        }

        public void UpdatePatient(int clientId, int patientId, EditPatientDto patient)
        {
            var dbPatient = _dbContext.Patients.Find(patientId);

            if (dbPatient.ClientId != clientId) throw new UnauthorizedException();

            _mapper.Map(patient, dbPatient);
            _dbContext.SaveChanges();
        }
    }
}
