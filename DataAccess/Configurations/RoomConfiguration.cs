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
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(r => r.RoomNumber)
                   .IsRequired()
                   .HasMaxLength(5);

            builder.HasIndex(r => new { r.HotelId, r.RoomNumber })
                   .IsUnique();

            builder.Property(r => r.Floor)
                   .HasColumnType("smallint")
                   .IsRequired();

            builder.ToTable(r =>
                r.HasCheckConstraint("CK_Room_Floor_NonNegative", "[Floor] >= 0"));

            builder.Property(r => r.PricePerNight)
                   .IsRequired()
                   .HasColumnType("decimal(10,2)");

            builder.ToTable(r =>
                r.HasCheckConstraint("CK_Room_PricePerNight_Positive", "[PricePerNight] > 0"));

            builder.Property(r => r.IsAvailable)
                   .HasDefaultValue(true);

            builder.Property(r => r.IsClean)
                   .HasDefaultValue(true);

            builder.Property(r => r.CreatedAt)
                   .HasDefaultValueSql("GETDATE()")
                   .HasColumnType("datetime2");

            // Relationships

            builder.HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.RoomType)
                .WithMany(rt => rt.Rooms)
                .HasForeignKey(r => r.RoomTypeId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
