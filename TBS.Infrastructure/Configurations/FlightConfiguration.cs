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
    public class FlightConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FlightNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.DepartureTime)
                .IsRequired();

            builder.Property(x => x.ArrivalTime)
                .IsRequired();

            builder.Property(x => x.Price)
                .IsRequired();

            builder.Property(x => x.StopCount)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.HasOne(x => x.Airline)
                .WithMany(x => x.Flights)
                .HasForeignKey(x => x.AirlineId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.DepartureAirport)
                .WithMany()
                .HasForeignKey(x => x.DepartureAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ArrivalAirport)
                .WithMany()
                .HasForeignKey(x => x.ArrivalAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x=> x.DepartureAirport)
                .WithMany(a=>a.DepartureFlights)
                .HasForeignKey(x => x.DepartureAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ArrivalAirport)
                .WithMany(a => a.ArrivalFlights)
                .HasForeignKey(x => x.ArrivalAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.FlightNumber)
            .IsUnique();
        }
    }
}
