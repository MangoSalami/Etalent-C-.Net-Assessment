


using Etalent_C__.Net_Assessment.Models;
using Microsoft.EntityFrameworkCore;
using static Etalent_C__.Net_Assessment.Models.BankAccount;

namespace Etalent_C__.Net_Assessment.Data
{
    public class AppDbcontext :DbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options) { }

       public DbSet<AccountHolder> AccountHolders { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
       public DbSet<Withdrawal> Withdrawals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Account Holders
            modelBuilder.Entity<AccountHolder>().HasData(
                new AccountHolder
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1985, 5, 20),
                    IdNumber = "8505201234088",
                    Address = "123 Main Street, Johannesburg",
                    Mobile = "+27831234567",
                    Email = "john.doe@example.com"
                },
                new AccountHolder
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    DateOfBirth = new DateTime(1990, 9, 15),
                    IdNumber = "9009155432084",
                    Address = "456 Oak Avenue, Cape Town",
                    Mobile = "+27839876543",
                    Email = "jane.smith@example.com"
                },
                new AccountHolder
                {
                    Id = 3,
                    FirstName = "Peter",
                    LastName = "Ngubane",
                    DateOfBirth = new DateTime(1978, 2, 2),
                    IdNumber = "7802025677081",
                    Address = "789 Pine Road, Durban",
                    Mobile = "+27831231234",
                    Email = "peter.ngubane@example.com"
                }
            );

            // Seed Bank Accounts
            modelBuilder.Entity<BankAccount>().HasData(
                new BankAccount
                {
                    Id = 1,
                    AccountNumber = 1001001001,
                    Type = AccountType.Cheque,
                    Name = "John's Cheque Account",
                    Status = AccountStatus.Active ,
                    AvailableBalance = 2500.00m,
                    AccountHolderId = 1
                },
                new BankAccount
                {
                    Id = 2,
                    AccountNumber = 1001001002,
                    Type = AccountType.Savings,
                    Name = "John's Savings",
                    Status = AccountStatus.Active,
                    AvailableBalance = 7800.50m,
                    AccountHolderId = 1
                },
                new BankAccount
                {
                    Id = 3,
                    AccountNumber = 1001002001,
                    Type = AccountType.Savings,
                    Name = "Jane's Savings",
                    Status = AccountStatus.Inactive,
                    AvailableBalance = 15000.00m,
                    AccountHolderId = 2
                },
                new BankAccount
                {
                    Id = 4,
                    AccountNumber = 1001002002,
                    Type = AccountType.FixedDeposit,
                    Name = "Jane's Fixed Deposit",
                    Status = AccountStatus.Active,
                    AvailableBalance = 20000.00m,
                    AccountHolderId = 2
                },
                new BankAccount
                {
                    Id = 5,
                    AccountNumber = 1001003001,
                    Type = AccountType.Cheque,
                    Name = "Peter's Cheque Account",
                    Status = AccountStatus.Inactive,
                    AvailableBalance = 500.00m,
                    AccountHolderId = 3
                }
            );
            modelBuilder.Entity<BankAccount>()
      .HasOne(b => b.AccountHolder)
      .WithMany(h => h.BankAccounts)
      .HasForeignKey(b => b.AccountHolderId);

            modelBuilder.Entity<Withdrawal>()
                .HasOne(w => w.BankAccount)
                .WithMany(b => b.Withdrawals)
                .HasForeignKey(w => w.BankAccountId);
        }


    }
}
