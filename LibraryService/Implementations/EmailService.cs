using LibraryService.Interfaces;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using LibraryDomain.Email;
using MimeKit.Text;
using Org.BouncyCastle.Security;
using Microsoft.Extensions.Options;
namespace LibraryService.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmail(EmailMessage message)
        {
           
            var email = new MimeMessage
            {
                Subject = message.Subject,
                Body = new TextPart(TextFormat.Plain)
                {
                    Text = message.Body
                },
                To = { new MailboxAddress(message.SendTo, message.SendTo) }
            };

            try
            {
                using var smtp = new SmtpClient();

                await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort); 
                await smtp.AuthenticateAsync(_emailSettings.SmtpUser, _emailSettings.SmtpPassword);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
