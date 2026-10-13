using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBS.Application.Common
{
    public class ValidationException:Exception
    {
        public IDictionary<string, string[]> Errors { get; }
        public ValidationException(IDictionary<string, string[]> errors):base("Bir və ya bir neçə doğrulama xətası baş verdi.")
        {
            Errors = errors;
        }
    }
}
