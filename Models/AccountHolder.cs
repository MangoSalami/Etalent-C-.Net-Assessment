namespace Etalent_C__.Net_Assessment.Models
{
    public class AccountHolder
    {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string IdNumber { get; set; }
            public string Address { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public ICollection<BankAccount> BankAccounts { get; set; }
        }
}
