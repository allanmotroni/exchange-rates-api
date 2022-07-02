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
   [Route("api/v1/transactions")]
   public class TransactionController : BaseController
   {
      private readonly IExchangeTransactionService _exchangeTransactionService;
      private readonly IMapper _mapper;

      public TransactionController(
         ICustomValidator customValidator, 
         ICustomLogger logger, 
         IExchangeTransactionService exchangeTransactionService, 
         IMapper mapper, 
         IUserService userService)
          : base(customValidator, logger, userService)
      {
         _exchangeTransactionService = exchangeTransactionService;
         _mapper = mapper;
      }

      /// <summary>
      /// fromCurrency = "EUR" (only)
      /// </summary>
      /// <param name="newTransactionDto"></param>
      /// <returns></returns>
      [HttpPost]
      [SwaggerResponse(StatusCodes.Status201Created)]
      [SwaggerResponse(StatusCodes.Status400BadRequest)]
      [SwaggerResponse(StatusCodes.Status401Unauthorized)]
      
      public async Task<IActionResult> Post([FromBody] NewTransactionDto newTransactionDto)
      {
         try
         {
            if (!await VerifyUser(newTransactionDto)) return Unauthorized();

            _logger.Info($"Starting a new transaction. {newTransactionDto}");

            Transaction transaction = _mapper.Map<Transaction>(newTransactionDto);
            await _exchangeTransactionService.Convert(transaction);

            TransactionDto transactionDto = null;
            if (IsValid)
            {
               transactionDto = _mapper.Map<TransactionDto>(transaction);
               return CreatedAtAction(nameof(ListByUserId), new { UserId = transactionDto.UserId }, transactionDto);
            }
            else
            {
               return CustomReponse(transactionDto);
            }
         }
         catch (Exception exception)
         {
            return CustomExceptionResponse(exception);
         }
      }

      [HttpGet]
      [Route("user/{userId:int}")]
      [SwaggerResponse(StatusCodes.Status200OK)]
      [SwaggerResponse(StatusCodes.Status400BadRequest)]
      [SwaggerResponse(StatusCodes.Status404NotFound)]
      [SwaggerResponse(StatusCodes.Status401Unauthorized)]
      public async Task<IActionResult> ListByUserId(int userId, [FromHeader] int userAuthenticated)
      {
         try
         {
            if (!await VerifyUser(userAuthenticated)) return Unauthorized();

            _logger.Info($"Listing all transactions by User. UserId: {userId}");

            IList<Transaction> list = await _exchangeTransactionService.ListByUserId(userId);

            if (list.Count == 0) NotFound();

            IEnumerable<TransactionDto> transactionsDto = null;
            if (IsValid)
               transactionsDto = _mapper.Map<IList<TransactionDto>>(list);

            return CustomReponse(transactionsDto);
         }
         catch (Exception exception)
         {
            return CustomExceptionResponse(exception);
         }
      }
   }
}
