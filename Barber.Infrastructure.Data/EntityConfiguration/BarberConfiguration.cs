using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barber.Infrastructure.Data.EntityConfiguration
{
    public class BarberConfiguration : IEntityTypeConfiguration<Barber.Domain.Entities.BarberMain>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.BarberMain> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).HasMaxLength(250).IsRequired(true);
            builder.Property(p => p.Disponibility).IsRequired(true);
            builder.HasMany(p => p.Schedules).WithOne(p => p._Barber).HasForeignKey(p => p.IdBarber);
        }
    }
}
