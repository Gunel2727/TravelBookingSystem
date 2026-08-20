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
    public class HotelBookingRoomPassengerConfiguration : IEntityTypeConfiguration<HotelBookingRoomPassenger>
    {
        public void Configure(EntityTypeBuilder<HotelBookingRoomPassenger> builder)
        {
            builder.HasKey(hbrp => new { hbrp.HotelBookingRoomId, hbrp.PassengerId });

            builder.HasOne(hbrp => hbrp.HotelBookingRoom)
                   .WithMany(hbr => hbr.HotelBookingRoomPassengers)
                   .HasForeignKey(hbrp => hbrp.HotelBookingRoomId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(hbrp => hbrp.Passenger)
                     .WithMany(p => p.HotelBookingRoomPassengers)
                     .HasForeignKey(hbrp => hbrp.PassengerId)
                     .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
