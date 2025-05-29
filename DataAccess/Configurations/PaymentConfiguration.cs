using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(10,2)");

            builder.ToTable(p => 
                p.HasCheckConstraint("CK_Payment_Amount_Positive", "[Amount] > 0"));

            builder.Property(p => p.Method)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(p => p.PaidAt)
                   .IsRequired()
                   .HasColumnType("datetime2");

            builder.Property(p => p.IsSuccessful)
                   .HasDefaultValue(true);

            // Relationships 
            builder.HasOne(p => p.Reservation)
                .WithMany(r => r.Payments)
                .HasForeignKey(p => p.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
