using HotelBooking.DTO;

namespace HotelBooking.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
        Task SendBookingConfirmationEmailAsync(string to, string userName, string bookingDetails);
    }
}
