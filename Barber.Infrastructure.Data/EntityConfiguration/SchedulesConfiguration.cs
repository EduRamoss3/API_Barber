using Barber.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barber.Infrastructure.Data.EntityConfiguration
{
    public class SchedulesConfiguration : IEntityTypeConfiguration<Schedules>
    {
        public void Configure(EntityTypeBuilder<Schedules> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.TypeOfService).IsRequired();
            builder.Property(p => p.DateSchedule).IsRequired();
            builder.Property(p => p.ValueForService).HasPrecision(10, 2).IsRequired();

            builder.HasOne(p => p._Barber)
                .WithMany(p => p.Schedules)
                .HasForeignKey(p => p.IdBarber)
                .HasConstraintName("Scheduled with one barber, barber with many schedules")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p._Client)
                .WithOne(c => c.Schedule)
                .HasForeignKey<Schedules>(s => s.IdClient)
                .HasConstraintName("Schedule with one client. Client with one schedule")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
