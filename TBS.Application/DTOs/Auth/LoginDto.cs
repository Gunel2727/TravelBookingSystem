using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBS.Application.DTOs.Auth
{
    public class LoginDto
    {
        public string Email { set; get; } = string.Empty;
        public string Password { set; get; } = string.Empty;
    }
}
