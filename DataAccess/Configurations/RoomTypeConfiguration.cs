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
    public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.ToTable("RoomTypes");

            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(rt => rt.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(rt => rt.Name)
                   .IsUnique();

            builder.Property(rt => rt.Capacity)
                   .HasColumnType("smallint")
                   .IsRequired();

            builder.ToTable(rt =>
                rt.HasCheckConstraint("CK_RoomType_Capacity_Positive", "[Capacity] > 0"));
        }
    }
}
