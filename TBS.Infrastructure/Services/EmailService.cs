using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.Interfaces;
using TBS.Infrastructure.Settings;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace TBS.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var messsage = new MimeMessage();
            messsage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            messsage.To.Add(new MailboxAddress("",toEmail));
            messsage.Subject = subject;
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body
            };
            messsage.Body = bodyBuilder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_emailSettings.Server, _emailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
                await client.SendAsync(messsage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
