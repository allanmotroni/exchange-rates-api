using ExchangeRates.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRates.Infrastructure.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> configuration)
            : base(configuration)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<User> User { get; set; }

    }
}
