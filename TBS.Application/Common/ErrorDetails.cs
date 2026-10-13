using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBS.Application.Common
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public IDictionary<string, string[]> Errors { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
