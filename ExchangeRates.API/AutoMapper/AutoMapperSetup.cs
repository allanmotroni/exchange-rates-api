using AutoMapper;
using ExchangeRates.Domain.Dto;
using ExchangeRates.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRates.API.AutoMapper
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<NewUserDto, User>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(p => true))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(p => DateTime.UtcNow));

        }
    }
}
