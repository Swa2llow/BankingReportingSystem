using ReportingSystem.DAL.Entities;
using System.Data.Entity;

namespace ReportingSystem.DAL.EF
{
    public class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(string connectionString)
                : base(connectionString)
        {
        }
        
        public virtual DbSet<CreditCards> CreditCards { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreditCards>()
                .Property(e => e.AvailableCash)
                .HasPrecision(10, 4);

            modelBuilder.Entity<CreditCards>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.CreditCard)
                .HasForeignKey(e => e.CardId);

            modelBuilder.Entity<Transactions>()
                .Property(e => e.Amount)
                .HasPrecision(10, 4);
        }
    }
}