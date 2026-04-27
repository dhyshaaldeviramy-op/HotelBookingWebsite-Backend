using HotelBookingWebsite_HCL.DTO;
using HotelBookingWebsite_HCL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingWebsite_HCL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDTO dto)
        {
            int userId = 1; // Temporary until JWT added

            var result = await _bookingService.CreateBookingAsync(userId, dto);

            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBookingsByUser(int userId)
        {
            var result = await _bookingService.GetBookingsByUserAsync(userId);

            return Ok(result);
        }
    }
}
