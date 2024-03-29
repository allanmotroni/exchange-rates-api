﻿using ExchangeRates.Application.Interfaces;
using ExchangeRates.Domain.Entities;
using ExchangeRates.Domain.Entities.Validations;
using ExchangeRates.Domain.Interfaces.Repositories;
using ExchangeRates.Domain.Validations;
using FluentValidation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRates.Application.Services
{
   public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ValidationService _validationService;
        private readonly ICustomValidator _customValidator;

        public UserService(IUserRepository userRepository, ValidationService validationService, ICustomValidator customValidator)
        {
            _userRepository = userRepository;
            _validationService = validationService;
            _customValidator = customValidator;
        }

        public async Task<User> FindById(int id)
        {
            User user = await _userRepository.GetById(id);
            if (user == null)
                _customValidator.Notify("User not found.");

            return user;
        }

        public async Task<IList<User>> FindAll()
        {
            IList<User> users = await _userRepository.GetAll();
            if (users.Count == 0)
                _customValidator.Notify("No Users were found.");

            return users;
        }

        public async Task<User> FindByEmail(string email)
        {
            User user = null;

            _validationService.Validate<string, AbstractValidator<string>>(email, new CustomEmailValidation());
            if (!_customValidator.HasErrors())
            {
                user = await _userRepository.GetByEmail(email);
                if (user == null)
                    _customValidator.Notify("User not found.");
            }

            return user;
        }

        public async Task Create(User user)
        {
            _validationService.Validate(user, new CreateUserValidation());
            if (!_customValidator.HasErrors())
            {
                User userCreated = await _userRepository.GetByEmail(user.Email);
                if (userCreated == null)
                {
                    await _userRepository.Create(user);
                }
                else
                {
                    _customValidator.Notify($"E-mail already registered. {user.Email}");
                }
            }
        }

    }
}
