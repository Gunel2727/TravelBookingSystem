using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Entitites;

namespace TBS.Infrastructure.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
              .IsRequired()
              .HasMaxLength(100);

            builder.HasIndex(x => new { x.CountryId, x.Name })
            .IsUnique();

            builder.HasOne(x=>x.Country)
                .WithMany(x=>x.Cities)
                .HasForeignKey(x=>x.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
