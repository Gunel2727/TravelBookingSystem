using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Entitites;

namespace TBS.Application.DTOs.Flight
{
    public class FlightDto
    {
        public Guid Id { get; set; }
        public string FlightNumber { get; set; } = string.Empty;
        public Guid DepartureAirportId { get; set; }
        public string DepartureAirportName { get; set; } = string.Empty;
        public Guid ArrivalAirportId { get; set; }
        public string ArrivalAirportName { get; set; } = string.Empty;
        public string AirlineName { get; set; } = string.Empty;
        public Guid AirlineId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } = string.Empty;
        public int StopCount { get; set; }
    }
    public class CreateFlightDto
    {
        public string FlightNumber { get; set; } = string.Empty;
        public Guid DepartureAirportId { get; set; }
        public Guid ArrivalAirportId { get; set; }
        public Guid AirlineId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
        public int StopCount { get; set; }
    }
    public class UpdateFlightDto
    {
        public string FlightNumber { get; set; } = string.Empty;
        public Guid DepartureAirportId { get; set; }
        public Guid ArrivalAirportId { get; set; }
        public Guid AirlineId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
        public int StopCount { get; set; }
    }
    public class FilterFlightDto
    {
        public Guid? DepartureAirportId { get; set; }
        public Guid? ArrivalAirportId { get; set; }
        public DateTime? DepartureDate { get; set; }
    }
}
