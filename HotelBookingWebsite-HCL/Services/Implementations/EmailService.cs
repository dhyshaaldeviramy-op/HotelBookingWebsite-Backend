using System.Net.Mail;
using System.Net;
using HotelBooking.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace HotelBooking.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly IConfiguration _configuration;

        public EmailService(ILogger<EmailService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            _logger.LogInformation("Attempting to send email to {To} with subject: {Subject}", to, subject);

            try
            {
                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                var port = int.Parse(_configuration["EmailSettings:Port"] ?? "587");
                var senderEmail = _configuration["EmailSettings:SenderEmail"];
                var senderName = _configuration["EmailSettings:SenderName"];
                var username = _configuration["EmailSettings:Username"];
                var password = _configuration["EmailSettings:Password"];

                if (string.IsNullOrEmpty(username) || username == "YOUR_GMAIL_USERNAME")
                {
                    _logger.LogWarning("Email settings are not configured. Mocking email sending.");
                    _logger.LogInformation("Email body: {Body}", body);
                    await Task.CompletedTask;
                    return;
                }

                using var message = new MailMessage();
                message.From = new MailAddress(senderEmail!, senderName);
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = false;

                using var client = new SmtpClient(smtpServer, port);
                client.Credentials = new NetworkCredential(username, password);
                client.EnableSsl = true;

                await client.SendMailAsync(message);
                _logger.LogInformation("Email sent successfully to {To}", to);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email to {To}", to);
                // We don't throw to avoid breaking the booking process
            }
        }

        public async Task SendBookingConfirmationEmailAsync(string to, string userName, string bookingDetails)
        {
            string subject = "Booking Confirmation - Hotel Booking Website";
            string body = $"Hello {userName},\n\nYour booking has been confirmed!\n\nDetails:\n{bookingDetails}\n\nThank you for choosing us!";
            
            await SendEmailAsync(to, subject, body);
        }
    }
}
