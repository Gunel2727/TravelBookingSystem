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
        public static readonly Guid BakuId = Guid.Parse("33333333-3333-3333-3333-333333333333");
        public static readonly Guid IstanbulId = Guid.Parse("44444444-4444-4444-4444-444444444444");
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

            builder.HasData(
                new City { Id = BakuId, Name = "Baku", CountryId = CountryConfiguration.AzerbaijanId },
                new City { Id = IstanbulId, Name = "Istanbul", CountryId = CountryConfiguration.TurkeyId }
            );
        }
    }
}
