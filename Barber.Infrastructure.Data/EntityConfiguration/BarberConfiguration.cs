
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
    public class BarberConfiguration : IEntityTypeConfiguration<Barber.Domain.Entities.Barber>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Barber> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).HasMaxLength(250).IsRequired(true);
            builder.Property(p => p.Disponibility).IsRequired(true);
            builder.HasMany(p => p.Schedules).WithOne(p => p._Barber).HasForeignKey(p => p.IdBarber);
        }
    }
}
