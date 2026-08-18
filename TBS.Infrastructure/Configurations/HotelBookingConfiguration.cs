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
    public class HotelBookingConfiguration : IEntityTypeConfiguration<HotelBooking>
    {
        public void Configure(EntityTypeBuilder<HotelBooking> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ConfirmationNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(x => x.ConfirmationNumber)
                .IsUnique();

            builder.Property(x => x.BookingDate)
                .IsRequired();
            builder.Property(x => x.CheckInDate)
                .IsRequired();
            builder.Property(x => x.CheckOutDate)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.HasOne(x => x.ApplicationUser)
                .WithMany()
                .HasForeignKey(x => x.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
