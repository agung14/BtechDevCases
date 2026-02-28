namespace AuthWithJWT_Net8.Models;

public class TransferTransaction
{
    public Guid TransactionId { get; set; }
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}