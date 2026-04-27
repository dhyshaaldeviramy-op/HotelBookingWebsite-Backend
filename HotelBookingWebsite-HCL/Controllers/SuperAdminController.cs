using HotelBooking.Data;
using HotelBooking.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "SuperAdmin")]
    public class SuperAdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SuperAdminController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("pending-owners")]
        public async Task<IActionResult> GetPendingOwners()
        {
            var pendingOwners = await _context.Users
                .Where(u => u.Role == "Owner" && u.Status == "Pending")
                .Select(u => new { u.userId, u.Name, u.email, u.Status })
                .ToListAsync();

            return Ok(pendingOwners);
        }

        [HttpGet("all-owners")]
        public async Task<IActionResult> GetAllOwners()
        {
            var allOwners = await _context.Users
                .Where(u => u.Role == "Owner")
                .Select(u => new { u.userId, u.Name, u.email, u.Status })
                .ToListAsync();

            return Ok(allOwners);
        }

        [HttpPost("approve-owner/{userId}")]
        public async Task<IActionResult> ApproveOwner(int userId)
        {
            var owner = await _context.Users.FindAsync(userId);
            if (owner == null || owner.Role != "Owner")
                return NotFound("Hotel owner not found.");

            owner.Status = "Approved";
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Hotel owner {owner.email} has been approved." });
        }

        [HttpPost("reject-owner/{userId}")]
        public async Task<IActionResult> RejectOwner(int userId)
        {
            var owner = await _context.Users.FindAsync(userId);
            if (owner == null || owner.Role != "Owner")
                return NotFound("Hotel owner not found.");

            owner.Status = "Rejected";
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Hotel owner {owner.email} has been rejected." });
        }
    }
}
