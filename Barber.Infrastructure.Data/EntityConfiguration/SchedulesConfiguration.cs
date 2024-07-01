using Barber.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


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
            builder.Property(p => p.IsFinalized).IsRequired();

            builder.HasOne(p => p._Barber)
                .WithMany(p => p.Schedules)
                .HasForeignKey(p => p.IdBarber)
                .HasConstraintName("Scheduled with one barber, barber with many schedules")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p._Client)
                .WithMany(p => p.Schedules)
                .HasForeignKey(p => p.IdClient)
                .HasConstraintName("Schedule with one client. Client with many schedule")
                .OnDelete(DeleteBehavior.Restrict);

            
        }
    }
}
