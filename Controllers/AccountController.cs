using System;
using Etalent_C__.Net_Assessment.Data;
using Etalent_C__.Net_Assessment.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using static Etalent_C__.Net_Assessment.Models.BankAccount;



[ApiController]
[Route("api/accounts")]
public class AccountController : ControllerBase
{

    public IBankRepository _repo;
    public AccountController(IBankRepository repo)
    {
        _repo = repo;
    }
    [Authorize]
    [HttpGet("/{holderId}")]
    public async Task<IActionResult> GetAccounts(int holderId) { 
      return  Ok(await _repo.GetAccountsByHolderId(holderId));
    }
    [Authorize]
    [HttpPost("/account")]
    public async Task<IActionResult> GetByAccountBuyAccNumber([FromBody] AccountDTO accountnumber)
    {
        return Ok(await _repo.GetAccountByAccountNumber(accountnumber.AccountNumber??0));
    }
    [Authorize]
    [HttpPost("/withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] WithdrawRequestDTO withdraw)
    {   var results=await _repo.Withdraw(withdraw.AccountNumber ?? 0, withdraw.amount??0);
       

        if (results.AccountNumber == null)
            return NotFound(results); 

        if (results.message.Equals("account is not active"))
            return StatusCode(403, results); // 403 Forbidden

        if (results.message.Equals("amount is less than zero"))
            return BadRequest(results); // 400

        if (results.message.Equals("amount is greater than balance"))
            return StatusCode(402, results); // 402 Payment Required (or 400)

        if (results.message.Equals("Account is fixed deposit and amount is not equal to the balance"))
            return UnprocessableEntity(results); // 422

        return Ok(results);

    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO req)
    {
        var result = await _repo.Login(req.email);

        if (result.Message.Equals("User doenst exist") )
            return Unauthorized(new {message = result.Message });

        return Ok(result);
    }
}
