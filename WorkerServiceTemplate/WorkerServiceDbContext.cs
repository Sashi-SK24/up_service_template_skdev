using Microsoft.EntityFrameworkCore;
using WorkerServiceTemplate.Models;

namespace WorkerServiceTemplate
{
    public class WorkerServiceDbContext : DbContext
    {
        public WorkerServiceDbContext(DbContextOptions<WorkerServiceDbContext> options) : base(options)
        {
        }
        public DbSet<Weather> Weathers { get; set; }
    }
}
