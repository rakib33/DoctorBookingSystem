using AutoMapper;
using DoctorBooking.Domain.Entities;
using DoctorBooking.Application.DTOs;

namespace DoctorBooking.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Clinic, ClinicDto>().ReverseMap();
            CreateMap<Doctor, DoctorDto>().ReverseMap();
            CreateMap<Patient, PatientDto>().ReverseMap();

            // Add more mappings here:
        }
    }
}
