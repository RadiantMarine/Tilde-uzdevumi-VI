using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Uzdevums2.Web.Models;

namespace Uzdevums2.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FinancialTransaction> FinancialTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<FinancialTransaction>()
                .ToTable("FinancialTransaction")
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,4)");
        }
    }
}
