using AutoMapper;
using DataAccess.Models;
using Services.Models.Patient;

namespace Services.Mappers
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, PatientDto>();
            CreateMap<EditPatientDto, Patient>();
        }
    }
}
