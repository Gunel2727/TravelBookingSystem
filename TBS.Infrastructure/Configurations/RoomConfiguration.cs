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
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
           builder.HasKey(e => e.Id);

            builder.HasIndex(e => new { e.HotelId, e.RoomNumber })
            .IsUnique();

            builder.Property(e => e.RoomNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.PriceForNight)
                .IsRequired()
                .HasPrecision(18,2);

            builder.Property(e => e.Capacity)
                .IsRequired();

            builder.Property(e => e.RoomType)
                .IsRequired();

            builder.HasOne(e=> e.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(e => e.HotelId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
