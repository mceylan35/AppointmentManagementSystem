using AppointmentManagementSystem.Application.DTOs;
using AppointmentManagementSystem.Application.DTOs.Services;
using AppointmentManagementSystem.Application.DTOs.Users;
using AppointmentManagementSystem.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace AppointmentManagementSystem.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(d => d.ServiceName, opt => opt.MapFrom(s => s.Service.Name))
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.User.Username))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));
            CreateMap<AppointmentDto, Appointment>();
            

           CreateMap<Service, ServiceDto>();
           CreateMap<User, UserDto>();
        }
    }
}
