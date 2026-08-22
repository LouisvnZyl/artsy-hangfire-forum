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
        }
    }
}
