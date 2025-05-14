using Etalent_C__.Net_Assessment.Data;
using Etalent_C__.Net_Assessment.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;



[ApiController]
[Route("api/accounts")]
public class AccountController : ControllerBase
{

    public BankRepository _repo;
    public AccountController(BankRepository repo)
    {
        _repo = repo;
    }
    [Authorize]
    [HttpGet("/{holderId}")]
    public async Task<IActionResult> GetAccounts(int holderId) { 
      return  Ok(await _repo.GetAccountsByHolderId(holderId));
    }
  
    [HttpPost("/account")]
    public async Task<IActionResult> GetByAccountBuyAccNumber([FromBody] AccountDTO accountnumber)
    {
        return Ok(await _repo.GetAccountByAccountNumber(accountnumber.AccountNumber));
    }

    [HttpPost("/withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] WithdrawDTO withdraw)
    {
        return Ok(await _repo.Withdraw(withdraw.AccountNumber,withdraw.amount));
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO req)
    {
        var result = await _repo.Login(req.email);

        if (result == null)
            return Unauthorized(new { message = "Invalid email or password" });

        return Ok(result);
    }
}
