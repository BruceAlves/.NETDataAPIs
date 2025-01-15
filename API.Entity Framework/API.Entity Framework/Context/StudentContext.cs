using Microsoft.EntityFrameworkCore;
using API.Entity_Framework.Models;

namespace API.Entity_Framework.Data
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
    }
}
