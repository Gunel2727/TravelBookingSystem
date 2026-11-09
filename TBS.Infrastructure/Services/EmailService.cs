using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.Interfaces;

namespace TBS.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string toEmail, string subject, string body)
        {
            throw new NotImplementedException();
        }
    }
}
