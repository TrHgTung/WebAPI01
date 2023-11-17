using WebAPI01.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPI01.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<CourseTeacher> CourseTeachers { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseCategory>().HasKey(pc => new { pc.CourseId, pc.CategoryId });
            modelBuilder.Entity<CourseCategory>().HasOne(p => p.Course).WithMany(pc => pc.CourseCategories).HasForeignKey(p => p.CourseId);
            modelBuilder.Entity<CourseCategory>().HasOne(p => p.Category).WithMany(pc => pc.CourseCategories).HasForeignKey(c => c.CategoryId);
            modelBuilder.Entity<CourseTeacher>().HasKey(po => new { po.CourseId, po.TeacherId });
            modelBuilder.Entity<CourseTeacher>().HasOne(p => p.Course).WithMany(pc => pc.CourseTeachers).HasForeignKey(p => p.CourseId);
            modelBuilder.Entity<CourseTeacher>().HasOne(p => p.Teacher).WithMany(pc => pc.CourseTeachers).HasForeignKey(c => c.TeacherId);
        }
    }
}
