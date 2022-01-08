using AutoMapper;
using DataAccess.Models;
using Services.Models.Appointment;

namespace Services.Mappers
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<CreateAppointmentDto, Appointment>();
            CreateMap<Appointment, AppointmentDto>();
        }
    }
}
