using HotelBooking.Data;
using HotelBooking.DTO;
using HotelBooking.Model;
using HotelBooking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> ProcessPaymentAsync(PaymentDTO dto)
        {
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.BookingId == dto.BookingId);

            if (booking == null)
                throw new Exception("Booking not found");

            var payment = new Payment
            {
                BookingId = dto.BookingId,
                Amount = dto.Amount,
                Method = dto.Method,
                Status = "Paid"
            };

            _context.Payments.Add(payment);

            booking.Status = "Paid";

            await _context.SaveChangesAsync();

            return "Payment Successful";
        }
    }
}