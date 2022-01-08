using AutoMapper;
using DataAccess.Models;
using Services.Models.Doctor;
using Services.Models.Patient;

namespace Services.Mappers
{
    public class ProcedureProfile : Profile
    {
        public ProcedureProfile()
        {
            CreateMap<Procedure, ProcedureDto>();
        }
    }
}
