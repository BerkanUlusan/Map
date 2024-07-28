using Microsoft.EntityFrameworkCore;

namespace MapApplication.Data
{
    public class MapApplicationDbContext : DbContext
    {
        public MapApplicationDbContext(DbContextOptions<MapApplicationDbContext> options) : base(options) { }

        public DbSet<Point> Points { get; set; }
    }
}
