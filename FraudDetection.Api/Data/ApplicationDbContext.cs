using FraudDetection.Api.Entities;
using Microsoft.EntityFrameworkCore;


namespace FraudDetection.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
   
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TransactionHistory> TransactionHistories { get; set; }
        public DbSet<User> Users { get; set; }
    }
}