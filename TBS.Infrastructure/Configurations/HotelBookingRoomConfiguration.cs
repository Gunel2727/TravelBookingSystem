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
    public class HotelBookingRoomConfiguration : IEntityTypeConfiguration<HotelBookingRoom>
    {
        public void Configure(EntityTypeBuilder<HotelBookingRoom> builder)
        {
            builder.HasKey(x => new { x.RoomId, x.HotelBookingId });

            builder.HasOne(x => x.Room)
                .WithMany()
                .HasForeignKey(x => x.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.HotelBooking)
                .WithMany(x => x.HotelBookingRooms)
                .HasForeignKey(x => x.HotelBookingId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
