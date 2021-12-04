using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lesson>(x =>
            {
                x.ToTable("lessons");
            });

            modelBuilder.Entity<Person>(x =>
            {
                x.ToTable("persons");
            });
            
            modelBuilder.Entity<Student>(x =>
            {
                x.ToTable("students");
            });
            
            modelBuilder.Entity<StudentScore>(x =>
            {
                x.ToTable("student_scores");
            });
            
            modelBuilder.Entity<Teacher>(x =>
            {
                x.ToTable("teachers");
            });
                
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentScore> StudentScores { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}