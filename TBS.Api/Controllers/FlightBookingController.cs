using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TBS.Application.Interfaces;

namespace TBS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightBookingController : ControllerBase
    {
        private readonly IFlightBookingService _flightBookingService;

        public FlightBookingController(IFlightBookingService flightBookingService)
        {
            _flightBookingService = flightBookingService;
        }


    }
}
