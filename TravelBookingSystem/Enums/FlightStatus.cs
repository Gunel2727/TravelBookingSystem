using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBS.Domain.Enums
{
    public enum FlightStatus
    {
        Scheduled=1,
        Delayed=2,
        Cancelled=3,
        Boarding=4,
        Departed=5,
        Arrived=6
    }
}
