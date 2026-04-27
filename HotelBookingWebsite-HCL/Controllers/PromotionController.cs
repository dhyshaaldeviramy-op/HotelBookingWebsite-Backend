using HotelBooking.DTO;
using HotelBooking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpPost("apply")]
        public async Task<IActionResult> ApplyPromotion(PromotionDTO dto)
        {
            var finalAmount = await _promotionService.ApplyPromotionAsync(dto);

            return Ok(new
            {
                OriginalAmount = dto.Amount,
                FinalAmount = finalAmount
            });
        }
    }
}