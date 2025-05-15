namespace Etalent_C__.Net_Assessment.Data
{
    public class WithdrawDTO : AccountDTO
    {
        public decimal amount { get; set; }
        public string message { get; set; }
    }
}
