using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AspNetMvcEFSobes.Models;


namespace AspNetMvcEFSobes.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Operation> Operations => Set<Operation>();
        public DbSet<Account> Accounts => Set<Account>();
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var accounts = new Account[]
            {
                new Account { Id = 1, PersonId = 1, MoneyRUB = 15M, isJuridicalPerson = true },
                new Account { Id = 2, PersonId = 1, MoneyRUB = 15M, isJuridicalPerson = true },
                new Account { Id = 3, PersonId = 2, MoneyRUB = 15M, isJuridicalPerson = false },
                new Account { Id = 4, PersonId = 3, MoneyRUB = 15M, isJuridicalPerson = false },
                new Account { Id = 5, PersonId = 3, MoneyRUB = 15M, isJuridicalPerson = false },
                new Account { Id = 6, PersonId = 4, MoneyRUB = 15M, isJuridicalPerson = true },
                new Account { Id = 7, PersonId = 5, MoneyRUB = 15M, isJuridicalPerson = true }
            };
            modelBuilder.Entity<Account>().HasData(accounts);

            var operations = new Operation[]
            {
                new Operation { Id = 1, isIncome = false, BalanceChange = -500M, DateTime = DateTime.Now, AccountId = 1 },
                new Operation { Id = 2, isIncome = false, BalanceChange = -200M, DateTime = DateTime.Now, AccountId = 2 },
                new Operation { Id = 3, isIncome = true, BalanceChange = 400M, DateTime = DateTime.Now, AccountId = 2 },
                new Operation { Id = 4, isIncome = true, BalanceChange = 700M, DateTime = DateTime.Now, AccountId = 3 },
                new Operation { Id = 5, isIncome = true, BalanceChange = 100M, DateTime = DateTime.Now, AccountId = 4 },
                new Operation { Id = 6, isIncome = true, BalanceChange = 88M, DateTime = DateTime.Now, AccountId = 5 },
                new Operation { Id = 7, isIncome = true, BalanceChange = 22M, DateTime = DateTime.Now, AccountId = 6 },
                new Operation { Id = 8, isIncome = false, BalanceChange = -1000M, DateTime = DateTime.Now, AccountId = 7 }
            };
            modelBuilder.Entity<Operation>().HasData(operations);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=testdb;Trusted_Connection=True;");
        }
    }
}
