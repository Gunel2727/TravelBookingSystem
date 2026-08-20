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
    public class FlightBookingPassengerConfiguration : IEntityTypeConfiguration<FlightBookingPassenger>
    {
        public void Configure(EntityTypeBuilder<FlightBookingPassenger> builder)
        {
            builder.HasKey(fbp => fbp.Id);

            builder.Property(fbp => fbp.SeatNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.HasIndex(x => new { x.FlightId, x.SeatNumber })
                .IsUnique();


            builder.HasOne(fbp => fbp.FlightBooking)
                .WithMany(fbp => fbp.FlightBookingPassengers)
                .HasForeignKey(fbp => fbp.FlightBookingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(fbp => fbp.Flight)
                .WithMany(f => f.FlightBookingPassengers)
                .HasForeignKey(fbp => fbp.FlightId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(fbp => fbp.Passenger)
                .WithMany(p => p.FlightBookingPassengers)
                .HasForeignKey(fbp => fbp.PassengerId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
