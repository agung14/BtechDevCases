using Microsoft.EntityFrameworkCore;
using AuthWithJWT_Net8.Models;

namespace AuthWithJWT_Net8.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<TransferTransaction> Transfers => Set<TransferTransaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId);

        modelBuilder.Entity<TransferTransaction>()
            .HasKey(t => t.TransactionId);
    }
}