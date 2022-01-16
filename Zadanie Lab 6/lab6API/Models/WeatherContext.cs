using Microsoft.EntityFrameworkCore;

namespace lab6API.Models
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options)
            : base(options)
        {
        }

        public DbSet<WheaterItem> TodoItems { get; set; }
    }
}
