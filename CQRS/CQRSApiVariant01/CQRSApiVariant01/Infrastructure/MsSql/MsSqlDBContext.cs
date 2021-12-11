using CQRSApiVariant01.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRSApiVariant01.Infrastructure.MsSql
{
    public class MsSqlDBContext : DbContext
    {
        public MsSqlDBContext(DbContextOptions<MsSqlDBContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
