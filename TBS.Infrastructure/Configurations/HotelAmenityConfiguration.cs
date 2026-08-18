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
    public class HotelAmenityConfiguration : IEntityTypeConfiguration<HotelAmenity>
    {
        public void Configure(EntityTypeBuilder<HotelAmenity> builder)
        {
            builder.HasKey(x => new { x.HotelId, x.AmenityId });

            builder.HasOne(x => x.Hotel)
                .WithMany(x => x.HotelAmenities)
                .HasForeignKey(x => x.HotelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Amenity)
                .WithMany(x => x.HotelAmenities)
                .HasForeignKey(x => x.AmenityId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
