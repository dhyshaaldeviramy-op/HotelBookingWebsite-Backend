using HotelBooking.DTO;
using HotelBooking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotels()
        {
            var result = await _hotelService.GetAllHotelsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var result = await _hotelService.GetHotelByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddHotel(CreateHotelDTO dto)
        {
            var result = await _hotelService.AddHotelAsync(dto);
            return Ok(result);
        }
    }
}