using ExchangeRates.Application.Interfaces;
using ExchangeRates.Domain.Entities;
using ExchangeRates.Domain.Entities.Validations;
using ExchangeRates.Domain.Interfaces.Repositories;
using ExchangeRates.Domain.Validations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRates.Application.Services
{
   public class ExchangeTransactionService : IExchangeTransactionService
    {
        private readonly ValidationService _validationService;
        private readonly ICustomValidator _customValidator;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IExchangeRateService _exchangeRateService;

        public ExchangeTransactionService(
           ValidationService validationService, 
           ICustomValidator customValidator, 
           ITransactionRepository transactionRepository, 
           IUserRepository userRepository, 
           IExchangeRateService exchangeRateService)
        {
            _validationService = validationService;
            _customValidator = customValidator;
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _exchangeRateService = exchangeRateService;
        }

        public async Task Convert(Transaction transaction)
        {
            _validationService.Validate(transaction, new CreateTransactionValidation());
            if (!_customValidator.HasErrors())
            {
                User user = await _userRepository.GetById(transaction.UserId);
                if (user == null)
                {
                    _customValidator.Notify($"User not found. UserId: {transaction.UserId}");
                }
                else
                {
                    double rate = await _exchangeRateService.GetExchangeRate(transaction.FromCurrency, transaction.ToCurrency);
                    if (transaction.CalculateRate(rate))
                        await _transactionRepository.Create(transaction);
                }
            }
        }

        public async Task<IList<Transaction>> ListByUserId(int userId)
        {
            IList<Transaction> transactions = new List<Transaction>();

            User user = await _userRepository.GetById(userId);
            if (user == null)
            {
                _customValidator.Notify($"User not found. UserId: {userId}");
            }
            else
            {
                transactions = await _transactionRepository.ListByUserId(userId);
                if (transactions.Count == 0)
                {
                    _customValidator.Notify($"No transactions were found to User. UserId: {userId}");
                }
            }

            return transactions;
        }
    }
}
