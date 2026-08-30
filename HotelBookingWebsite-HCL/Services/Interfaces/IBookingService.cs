using HotelBookingWebsite_HCL.DTO;

namespace HotelBookingWebsite_HCL.Services.Interfaces
{
    public interface IBookingService
    {
        Task<string> CreateBookingAsync(int userId, CreateBookingDTO dto);
        Task<List<BookingDTO>> GetBookingsByUserAsync(int userId);
    }
}
