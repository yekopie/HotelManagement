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
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(r => r.CheckInDate)
                   .IsRequired()
                   .HasColumnType("date");

            builder.Property(r => r.CheckOutDate)
                   .IsRequired()
                   .HasColumnType("date");

            builder.ToTable(r => 
                r.HasCheckConstraint("CK_Reservation_CheckOut_After_CheckIn", "[CheckOutDate] > [CheckInDate]"));

            builder.Property(r => r.Status)
                   .HasConversion<byte>() // Enum 
                   .IsRequired();

            builder.Property(r => r.TotalAmount)
                   .IsRequired()
                   .HasColumnType("decimal(10,2)");

            builder.ToTable(r =>
                r.HasCheckConstraint("CK_Reservation_TotalAmount_NonNegative", "[TotalAmount] >= 0"));


            //Relationship

            builder.HasOne(r => r.Guest)
                .WithMany(g => g.Reservations)
                .HasForeignKey(r => r.GuestId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Room)
                .WithMany(rm => rm.Reservations)
                .HasForeignKey(r => r.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
