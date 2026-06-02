using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Transaction> Transactions => Set<Transaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Amount).HasPrecision(18, 2);
                entity.Property(t => t.Category).HasMaxLength(100).IsRequired();
                entity.Property(t => t.Type).HasConversion<string>(); // stores "Expense" not 0
            });
        }
    }
}
