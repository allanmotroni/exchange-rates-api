namespace ExchangeRates.Domain.API
{
    public class UserDto
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
