namespace ExchangeRates.Application.Dto
{
    public class NewUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} - Email: {Email}";
        }
    }
}
