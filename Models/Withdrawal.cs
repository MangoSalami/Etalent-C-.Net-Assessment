namespace Etalent_C__.Net_Assessment.Models
{
    public class Withdrawal
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }
    }
}
