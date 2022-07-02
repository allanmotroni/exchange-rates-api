using AutoMapper;
using ExchangeRates.Application.Dto;
using ExchangeRates.Application.Interfaces;
using ExchangeRates.Domain.Entities;
using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRates.API.Controllers
{
   [Route("api/v1/symbols")]
   public class SymbolController : BaseController
   {
      private readonly ISymbolService _symbolService;
      private readonly IMapper _mapper;

      public SymbolController(
          ICustomValidator customValidator,
          IUserService userService,
          ICustomLogger logger,
          IMapper mapper,
          ISymbolService symbolService)
          : base(customValidator, logger, userService)
      {
         _mapper = mapper;
         _symbolService = symbolService;
      }

      [HttpGet]
      [SwaggerResponse(StatusCodes.Status200OK)]
      [SwaggerResponse(StatusCodes.Status400BadRequest)]
      [SwaggerResponse(StatusCodes.Status401Unauthorized)]
      [SwaggerResponse(StatusCodes.Status404NotFound)]
      public async Task<IActionResult> GetAll([FromHeader] int userAuthenticated)
      {
         try
         {
            _logger.Info("Listing all symbols");

            if (!await VerifyUser(userAuthenticated)) return Unauthorized();

            IList<Symbol> list = await _symbolService.GetAll();
            
            if (list.Count == 0) NotFound();
            
            IList<SymbolDto> listDto = new List<SymbolDto>();
            if (IsValid)
               listDto = _mapper.Map<IList<SymbolDto>>(list);

            return CustomReponse(listDto);
         }
         catch (Exception ex)
         {
            return CustomExceptionResponse(ex);
         }
      }

   }
}