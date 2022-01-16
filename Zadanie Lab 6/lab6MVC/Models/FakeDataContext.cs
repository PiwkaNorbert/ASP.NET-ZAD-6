using lab6API.Models;
using Microsoft.EntityFrameworkCore;

namespace lab6MVC.Models
{
    public class FakeContext : DbContext
    {
        public FakeContext(DbContextOptions<FakeContext> options)
            : base(options)
        {
        }

        public DbSet<WheaterItem> TodoItems { get; set; }

        public DbSet<lab6API.Models.WheaterItem> TodoItem { get; set; }

    }
}
