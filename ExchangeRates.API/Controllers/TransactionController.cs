using AutoMapper;
using ExchangeRates.API;
using ExchangeRates.Domain.Entities;
using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Interfaces.Services;
using ExchangeRates.Domain.Validations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRates.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : MyControllerBase
    {
        private readonly IExchangeTransactionService _exchangeTransactionService;
        private readonly IMapper _mapper;
        public TransactionController(ICustomValidator customValidator, ICustomLogger logger, IExchangeTransactionService exchangeTransactionService, IMapper mapper)
            : base(customValidator, logger) 
        {
            _exchangeTransactionService = exchangeTransactionService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Convert([FromBody] NewTransactionDto newTransactionDto)
        {
            try
            {
                _logger.Info($"Starting a new transaction. {newTransactionDto}");

                Transaction transaction = _mapper.Map<Transaction>(newTransactionDto);
                await _exchangeTransactionService.Convert(transaction);

                TransactionDto transactionDto = null;
                if (IsValid)
                    transactionDto = _mapper.Map<TransactionDto>(transaction);

                return CustomReponse(transactionDto);
            }
            catch (Exception exception)
            {
                return CustomExceptionResponse(exception);
            }
        }

        [HttpGet]
        [Route("[action]/{userId:int}")]
        public async Task<IActionResult> ListByUserId(int userId)
        {
            try
            {
                _logger.Info($"Listing all transactions by User. UserId: {userId}");

                IEnumerable<Transaction> transactions = await _exchangeTransactionService.ListByUserId(userId);

                IEnumerable<TransactionDto> transactionsDto = null;
                if (IsValid)
                    transactionsDto = _mapper.Map<IList<TransactionDto>>(transactions);

                return CustomReponse(transactionsDto);
            }
            catch (Exception exception)
            {
                return CustomExceptionResponse(exception);
            }
        }
    }
}
