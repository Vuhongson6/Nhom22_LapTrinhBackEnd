using Microsoft.EntityFrameworkCore;
using museum_artifact_manager.Models;

namespace museum_artifact_manager.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Bảng Artifact (Hiện vật)
        public DbSet<Artifact> Artifacts { get; set; }

        // Bảng Exhibition (Triển lãm)
        public DbSet<Exhibition> Exhibitions { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Restoration> Restorations { get; set; }
        
    }
}