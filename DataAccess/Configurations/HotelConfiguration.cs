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
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.ToTable("Hotels");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.Id)
                .ValueGeneratedOnAdd();

            builder.Property(h => h.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(h => h.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(h => h.Address)
                .IsRequired()
                .HasMaxLength(255);


            builder.Property(h => h.Phone)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(h => h.StarRating)
                .HasColumnType("tinyint");

            builder.ToTable(h =>
                h.HasCheckConstraint("CK_Hotel_StarRating", "[StarRating] >= 1 AND [StarRating] <= 5"));

            builder.Property(h => h.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.HasIndex(h => new { h.Name, h.City }).IsUnique();

            
        }
    }
}
