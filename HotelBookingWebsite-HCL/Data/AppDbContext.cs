using HotelBooking.Model;
using Microsoft.EntityFrameworkCore;


namespace HotelBooking.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
