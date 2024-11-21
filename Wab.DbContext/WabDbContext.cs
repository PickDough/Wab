using Microsoft.EntityFrameworkCore;

namespace Wab.DbContext;

public class WabDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public WabDbContext(DbContextOptions<WabDbContext> options) : base(options)
    {
    }
}