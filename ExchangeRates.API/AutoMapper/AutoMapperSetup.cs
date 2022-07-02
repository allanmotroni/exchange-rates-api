using AutoMapper;
using ExchangeRates.Application.Dto;
using ExchangeRates.Domain.Entities;
using System;

namespace ExchangeRates.API.AutoMapper
{
   public class AutoMapperSetup : Profile
   {
      public AutoMapperSetup()
      {
         CreateMap<User, UserDto>();
         CreateMap<NewUserDto, User>()
             .ForMember(dest => dest.Active, opt => opt.MapFrom(p => true))
             .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(p => DateTime.UtcNow));

         CreateMap<Transaction, TransactionDto>();
         CreateMap<NewTransactionDto, Transaction>()
             .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(p => DateTime.UtcNow));

         CreateMap<Symbol, SymbolDto>();
      }
   }
}
