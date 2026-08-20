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
    public class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
    {
        public void Configure(EntityTypeBuilder<Passenger> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength (50);

            builder.Property (x => x.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.DateOfBirth)
                .IsRequired();

            builder.Property(x => x.PassportNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(x=>x.PassportNumber)
                .IsUnique();

            builder.Property(x => x.Gender)
                .IsRequired();

            builder.Property(x => x.Nationality)
                .IsRequired();
            
            builder.Property(x => x.PassportExpiryDate)
                .IsRequired();

        }
    }
}
