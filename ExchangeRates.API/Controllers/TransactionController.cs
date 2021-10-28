using AutoMapper;
using ExchangeRates.Domain.Entities;
using ExchangeRates.Domain.Interfaces.Logger;
using ExchangeRates.Domain.Interfaces.Services;
using ExchangeRates.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExchangeRates.API.Controllers
{
    [Route("v1/api/transactions")]    
    public class TransactionController : BaseController
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
        public async Task<IHttpActionResult> Post([FromBody] NewTransactionDto newTransactionDto)
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
        [Route("user/{userId:int}")]
        public async Task<IHttpActionResult> ListByUserId(int userId)
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
