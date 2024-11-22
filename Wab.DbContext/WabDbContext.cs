using Microsoft.EntityFrameworkCore;
using Wab.DbContext.Entity;

namespace Wab.DbContext;

public class WabDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public WabDbContext(DbContextOptions<WabDbContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CardEntity> Cards { get; set; }
    public DbSet<PaymentDueEntity> PaymentDues { get; set; }
    public DbSet<TransactionEntity> Transactions { get; set; }
}