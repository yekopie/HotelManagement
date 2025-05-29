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
    public class MaintanceConfiguration : IEntityTypeConfiguration<Maintenance>
    {
        public void Configure(EntityTypeBuilder<Maintenance> builder)
        {

            builder.ToTable("Maintenances");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(m => m.Type)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(m => m.Status)
                   .IsRequired()
                   .HasMaxLength(15);

            builder.Property(m => m.ScheduledDate)
                   .IsRequired()
                   .HasColumnType("date");

            builder.Property(m => m.CompletedDate)
                   .HasColumnType("date");

            builder.Property(m => m.Note)
                   .HasMaxLength(255);

            builder.ToTable(m => 
                m.HasCheckConstraint("CK_Maintenance_ScheduledDate", "[ScheduledDate] >= CAST(GETDATE() AS DATE)"));


            //Relationships

            builder.HasOne(m => m.Room)
                .WithMany(r => r.Maintenances)
                .HasForeignKey(m => m.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
