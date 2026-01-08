using Largest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Largest.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Balance> Balances => Set<Balance>();
        public DbSet<BalanceUser> BalanceUsers => Set<BalanceUser>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Transaction> Transactions => Set<Transaction>();
        public DbSet<ExportJob> ExportJobs => Set<ExportJob>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BalanceUser>()
                .HasIndex(x => new { x.BalanceId, x.UserId })
                .IsUnique();

            modelBuilder.Entity<BalanceUser>()
                .HasOne(x => x.Balance)
                .WithMany(b => b.BalanceUsers)
                .HasForeignKey(x => x.BalanceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BalanceUser>()
                .HasOne(x => x.User)
                .WithMany(u => u.BalanceUsers)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Balance)
                .WithMany(b => b.Transactions)
                .HasForeignKey(t => t.BalanceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Category)
                .WithMany()
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }

}