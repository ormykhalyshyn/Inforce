using Microsoft.EntityFrameworkCore;

namespace TaskI.Models
{
    public class TaskDbContext : DbContext
    {

        public TaskDbContext(DbContextOptions<TaskDbContext> options): base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<ShortUrl> ShortUrls { get; set; }

    }

}
