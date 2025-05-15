

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Etalent_C__.Net_Assessment.Data;
using Etalent_C__.Net_Assessment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static Etalent_C__.Net_Assessment.Models.BankAccount;
using Microsoft.Extensions.Configuration;

namespace Etalent_C__.Net_Assessment.Repo
{
    public class BankRepository: IBankRepository
    {
        private readonly AppDbcontext _context;
        private readonly IConfiguration _config;
        public BankRepository(AppDbcontext context, IConfiguration config) {
            _config=config;
        _context = context;
        }
        public async Task<IEnumerable<BankAccount>> GetAccountsByHolderId(int holderId)
        {
         

            return await _context.BankAccounts
                .Where(a => a.AccountHolderId== holderId)
            .ToListAsync();
        }
       

        public async Task<BankAccount?> GetAccountByAccountNumber(int accountNumber)
        {
            return await _context.BankAccounts
                .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }

        public async Task<WithdrawDTO> Withdraw(int accountId, decimal amount)
        {
            var account = await GetAccountByAccountNumber(accountId);
            if (account == null) { return new WithdrawDTO { AccountNumber=null,amount=amount ,message="account does not exist"}; }
            if (account.Status == AccountStatus.Inactive) {
                return new WithdrawDTO { AccountNumber = null, amount = amount, message = "account is not active" };
            } if (amount <= 0) {
                return new WithdrawDTO { AccountNumber = null, amount = amount, message = "amount is less than zero" };
            } if(amount > account.AvailableBalance) { return new WithdrawDTO { AccountNumber = null, amount = amount, message = "amount is greater than balance" }; }
             

            if (account.Type == AccountType.FixedDeposit && amount != account.AvailableBalance)
            { return new WithdrawDTO { AccountNumber = null, amount = amount, message = "Account is fixed deposit and amount is not equal to the balance" }; }

            account.AvailableBalance -= amount;

            // log withdrawal
            _context.Withdrawals.Add(new Withdrawal
            {
                BankAccountId= account.Id,
                Amount = amount,
                Date = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return new WithdrawDTO { AccountNumber=account.AccountNumber,amount=amount,message="withdrawal successful"};
        }
        public async Task<LoginResponseDTO?> Login(string email)
        {
            // 1) look up user by email
            var user = await _context.AccountHolders
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

            if (user == null) return new LoginResponseDTO
            {
                Token = null,
                Expires = DateTime.Now,
                Message="User doenst exist"
            }; ;

         

            // 3) generate JWT
            var jwtCfg = _config.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtCfg["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
           
        };

            var expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtCfg["DurationMinutes"]!));

            var token = new JwtSecurityToken(
                issuer: jwtCfg["Issuer"],
                audience: jwtCfg["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new LoginResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = expires,
                Message = "Success"
            };
        }

    }
}
