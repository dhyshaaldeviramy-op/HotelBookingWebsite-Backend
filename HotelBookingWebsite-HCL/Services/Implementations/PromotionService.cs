using HotelBooking.Data;
using HotelBooking.DTO;
using HotelBooking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Services.Implementations
{
    public class PromotionService : IPromotionService
    {
        private readonly AppDbContext _context;

        public PromotionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> ApplyPromotionAsync(PromotionDTO dto)
        {
            var promo = await _context.Promotions
                .FirstOrDefaultAsync(p =>
                    p.Code == dto.Code &&
                    p.ExpiryDate >= DateTime.Now);

            if (promo == null)
                throw new Exception("Invalid or Expired Promo Code");

            int discount = dto.Amount * promo.DiscountPercentage / 100;

            return dto.Amount - discount;
        }
    }
}