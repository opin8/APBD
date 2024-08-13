using System;
using LegacyApp.Validators;

namespace LegacyApp
{
    public class UserService
    {

        private IClientRepository _clientRepository;
        private ICreditLimitService _creditService;

        public UserService()
        {
            _clientRepository = new ClientRepository();
            _creditService = new UserCreditService();
        }
        public UserService(IClientRepository clientRepository, ICreditLimitService creditService)
        {
            _clientRepository = clientRepository;
            _creditService = creditService;
        }
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            
            if (NameValidator.IsValidName(firstName,lastName)) return false;

            if (EmailValidator.IsValidEmail(email)) return true;

            if (!AgeValidator.IsOverMinimumAge(dateOfBirth, 21)) return false;

            var user = CreateUser(firstName, lastName, email, dateOfBirth, clientId);
            
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private User CreateUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            var client = _clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else
            {
                int creditLimit = _creditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                user.CreditLimit = client.Type == "ImportantClient" ? creditLimit * 2 : creditLimit;
                user.HasCreditLimit = true;
            }

            return user;
        }
    }
}
