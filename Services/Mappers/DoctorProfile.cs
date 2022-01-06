using AutoMapper;
using DataAccess.Models;
using Services.Models.Doctor;

namespace Services.Mappers
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<Doctor, DoctorDto>();
            CreateMap<EditDoctorDto, Doctor>();
        }
    }
}
