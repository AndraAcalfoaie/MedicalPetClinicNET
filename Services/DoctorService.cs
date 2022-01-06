using AutoMapper;
using DataAccess;
using DataAccess.Models;
using Services.Models.Doctor;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public interface IDoctorService
    {
        List<DoctorDto> GetAll();
        void Add(EditDoctorDto doctor);
        void Update(int doctorId, EditDoctorDto doctor);
        void Detele(int doctorId);
    }

    public class DoctorService : IDoctorService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public DoctorService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public void Add(EditDoctorDto doctor)
        {
            var dbDoctor = _mapper.Map<Doctor>(doctor);
            _dbContext.Doctors.Add(dbDoctor);
            _dbContext.SaveChanges();
        }

        public void Detele(int doctorId)
        {
            var doctor = _dbContext.Doctors.First(x => x.Id == doctorId);
            _dbContext.Doctors.Remove(doctor);
            _dbContext.SaveChanges();
        }

        public List<DoctorDto> GetAll()
        {
            var dbDoctors = _dbContext.Doctors.ToList();
            return _mapper.Map<List<DoctorDto>>(dbDoctors);
        }

        public void Update(int doctorId, EditDoctorDto doctor)
        {
            var dbDoctor = _dbContext.Doctors.Find(doctorId);
            _mapper.Map(doctor, dbDoctor);

            _dbContext.SaveChanges();
        }
    }
}
