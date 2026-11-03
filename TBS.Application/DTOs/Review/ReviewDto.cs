using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBS.Application.DTOs.Review
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public Guid AppLicationUserId { get; set; }
        public string ApplicationUserName { get; set; } = string.Empty;
        public Guid HotelId { get; set; }
        public string HotelName { get; set;} = string.Empty;
    }

    public class CreateReviewDto
    {
        public Guid HotelId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
