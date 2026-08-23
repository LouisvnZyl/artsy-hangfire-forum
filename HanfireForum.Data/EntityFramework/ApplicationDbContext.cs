using HanfireForum.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HanfireForum.Data.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PaymentRequestModel> Payments => Set<PaymentRequestModel>();
        public DbSet<SuspenseTransactionModel> SuspenseTransactions { get; set; }

        public DbSet<PaymentSubmissionModel> PaymentSubmissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PaymentRequestModel>(entity =>
            {
                entity.HasKey(x => x.PaymentId);

                entity.Property(x => x.PaymentId)
                    .ValueGeneratedOnAdd();

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(x => x.Value)
                    .HasPrecision(18, 2);

                entity.Property(x => x.SubmissionDate)
                    .IsRequired();

                entity.Property(x => x.Status)
                    .HasConversion<string>()
                    .IsRequired();
            });

            modelBuilder.Entity<SuspenseTransactionModel>()
                        .HasKey(x => x.Id);

            modelBuilder.Entity<SuspenseTransactionModel>()
                        .HasOne(x => x.Payment)
                        .WithOne(x => x.SuspenseTransaction)
                        .HasForeignKey<SuspenseTransactionModel>(x => x.PaymentId);

            modelBuilder.Entity<PaymentSubmissionModel>()
                        .HasKey(x => x.Id);

            modelBuilder.Entity<PaymentSubmissionModel>()
                        .HasOne(x => x.Payment)
                        .WithOne(x => x.PaymentSubmission)
                        .HasForeignKey<PaymentSubmissionModel>(x => x.PaymentId);
        }
    }
}
