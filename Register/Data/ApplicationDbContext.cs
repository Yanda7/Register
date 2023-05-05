using Microsoft.EntityFrameworkCore;
using Register.Models;

namespace Register.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }
        public DbSet<Person> Persons { get; set; }


    }
}
