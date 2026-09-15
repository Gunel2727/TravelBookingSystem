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
    public class AirportConfiguration : IEntityTypeConfiguration<Airport>
    {
        public static readonly Guid GydAirportId = Guid.Parse("55555555-5555-5555-5555-555555555555");
        public static readonly Guid IstAirportId = Guid.Parse("66666666-6666-6666-6666-666666666666");
        public void Configure(EntityTypeBuilder<Airport> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(10);

            builder.HasIndex(x => x.Code)
             .IsUnique();

            builder.HasOne(x=>x.City)
                .WithMany(x=>x.Airports)
                .HasForeignKey(x=> x.CityId)
                .OnDelete(DeleteBehavior.Restrict);


          

            builder.HasData(
                new Airport
                {
                    Id = GydAirportId,
                    Name = "Heydar Aliyev International Airport",
                    Code = "GYD",
                    CityId = CityConfiguration.BakuId
                },
                new Airport
                {
                    Id = IstAirportId,
                    Name = "Istanbul Airport",
                    Code = "IST",
                    CityId = CityConfiguration.IstanbulId
                }
            );

        }
    }
}
