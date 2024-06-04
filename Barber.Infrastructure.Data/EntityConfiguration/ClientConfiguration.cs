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
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).HasMaxLength(250).IsRequired();
            builder.Property(p => p.Points).HasColumnOrder(2).IsRequired();
            builder.Property(p => p.LastTimeHere);
            builder.Property(p => p.Points).HasColumnOrder(3);
            builder.Property(p => p.Scheduled).IsRequired();
        }
    }
}
