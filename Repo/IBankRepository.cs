using Etalent_C__.Net_Assessment.Data;
using Etalent_C__.Net_Assessment.Models;

namespace Etalent_C__.Net_Assessment.Repo
{
    public interface IBankRepository
    {
        Task<IEnumerable<BankAccount>> GetAccountsByHolderId(int holderId);
        Task<BankAccount?> GetAccountByAccountNumber(int accountId);
        Task<WithdrawDTO> Withdraw(int accountId, decimal amount);
        Task<LoginResponseDTO?> Login(string email);
    }
}
