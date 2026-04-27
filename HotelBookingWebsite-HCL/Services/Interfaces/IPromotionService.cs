using HotelBooking.DTO;

namespace HotelBooking.Services.Interfaces
{
    public interface IPromotionService
    {
        Task<int> ApplyPromotionAsync(PromotionDTO dto);
    }
}