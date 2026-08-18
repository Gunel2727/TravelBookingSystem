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
            throw new NotImplementedException();
        }
    }
}
