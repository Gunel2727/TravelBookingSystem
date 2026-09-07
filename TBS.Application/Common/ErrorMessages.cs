using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBS.Application.Common
{
    public static class ErrorMessages
    {
        public const string FlightNotFound = "Axtarılan uçuş tapılmadı.";
        public const string AirportNotFound = "Hava limanı tapılmadı.";
        public const string InvalidFlightDates = "Eniş tarixi qalxış tarixindən əvvəl ola bilməz.";
        public const string HotelNotFound = "Axtarılan otel tapılmadı.";
        public const string RoomNotFound = "Axtarılan otaq tapılmadı.";
        public const string HotelBookingNotFound = "Axtarılan otel rezervasiyası tapılmadı.";
        public const string RoomNotAvailable = "Seçilmiş otaq mövcud deyil.";
        public const string CheckInDateCannotBeInThePast = "Giriş tarixi keçmiş tarix ola bilməz";
        public const string CheckOutDateMustBeAfterCheckInDate = "Çıxış tarixi giriş tarixindən sonra olmalıdır";
    }
}

