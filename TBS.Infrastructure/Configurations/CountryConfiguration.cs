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
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public static readonly Guid AzerbaijanId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        public static readonly Guid TurkeyId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        public void Configure(EntityTypeBuilder<Country> builder)
        { 
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(10);

            builder.HasIndex(x => x.Name)
            .IsUnique();


            builder.HasIndex(x => x.Code)
            .IsUnique();

            builder.HasData(
                new Country { Id = AzerbaijanId, Name = "Azerbaijan", Code = "AZ" },
                new Country { Id = TurkeyId, Name = "Turkey", Code = "TR" }
            );

        }
    }
}
