using Microsoft.EntityFrameworkCore;
using MyAPIProject.Models;

namespace MyAPIProject.Data
{
    public class CarsAPIDbContext:DbContext
    {
        public CarsAPIDbContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<Cars> cars { get; set; }
    }
}
