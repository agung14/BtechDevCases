using AuthWithJWT_Net8.Data;
using AuthWithJWT_Net8.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TransactionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TransactionsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("transfer")]
    public async Task<IActionResult> Transfer([FromBody] TransferRequest request)
    {
        if (request.Amount <= 0)
            return BadRequest("Amount must be greater than zero.");

        var userIdClaim = User.FindFirst("userId")?.Value;
        if (userIdClaim == null)
            return Unauthorized();

        var senderId = Guid.Parse(userIdClaim);

        using var dbTransaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var sender = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == senderId);

            if (sender == null)
                return NotFound("Sender not found.");

            var recipient = await _context.Users
                .FirstOrDefaultAsync(u => u.Account == request.RecipientAccount);

            if (recipient == null)
                return NotFound("Recipient not found.");

            if (sender.Balance < request.Amount)
                return BadRequest("Insufficient balance.");

            // Kurangi saldo sender
            sender.Balance -= request.Amount;

            // Tambah saldo recipient
            recipient.Balance += request.Amount;

            var transaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                SenderId = sender.UserId,
                ReceiverId = recipient.UserId,
                Amount = request.Amount,
                CreatedAt = DateTime.UtcNow
            };

            _context.Transactions.Add(transaction);

            await _context.SaveChangesAsync();
            await dbTransaction.CommitAsync();

            return Ok(new
            {
                message = "Transfer successful",
                senderBalance = sender.Balance
            });
        }
        catch
        {
            await dbTransaction.RollbackAsync();
            return StatusCode(500, "Transaction failed.");
        }
    }
}