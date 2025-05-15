using HeThongDatBan.Models;
using Microsoft.EntityFrameworkCore;
namespace HeThongDatBan.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Khai báo DbSet cho bảng trong database
        public DbSet<User> Users { get; set; }
    }
}
