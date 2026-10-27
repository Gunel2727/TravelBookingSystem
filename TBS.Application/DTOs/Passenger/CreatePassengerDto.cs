using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Enums;

namespace TBS.Application.DTOs.Passenger
{
    public class CreatePassengerDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string PassportNumber { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public string Nationality { get; set; } = string.Empty;
        public DateTime PassportExpiryDate { get; set; }
    }
}
