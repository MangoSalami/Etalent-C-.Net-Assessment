using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Etalent_C__.Net_Assessment.Models
{
    public class BankAccount
    {
        public enum AccountType { Cheque, Savings, FixedDeposit }
        public enum AccountStatus { Active, Inactive }
        public int Id { get; set; }
        public int AccountNumber { get; set; } 
        public AccountType Type { get; set; }
        public string Name { get; set; }
        public AccountStatus Status { get; set; }
        public decimal AvailableBalance { get; set; }

        public int AccountHolderId { get; set; }
     
        public AccountHolder AccountHolder { get; set; }

        public ICollection<Withdrawal> Withdrawals { get; set; }
    }
}
