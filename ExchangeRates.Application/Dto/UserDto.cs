using ExchangeRates.Application.Interfaces;

namespace ExchangeRates.Application.Dto
{
   public class UserDto : IUser
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"Id: {UserId} - Name: {Name} - Email: {Email}";
        }

    }
}
