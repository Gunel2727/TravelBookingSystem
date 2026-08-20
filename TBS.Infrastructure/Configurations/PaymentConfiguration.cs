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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(x => x.PaymentDate)
                .IsRequired();

            builder.Property(x => x.PaymentMethod)
                .IsRequired();
                

            builder.Property(x => x.Currency)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.Provider)
                .IsRequired();

            builder.Property(x => x.ProviderTransactionId)
                .IsRequired();

            builder.HasOne(x => x.FlightBooking)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.FlightBookingId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.HotelBooking)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.HotelBookingId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
