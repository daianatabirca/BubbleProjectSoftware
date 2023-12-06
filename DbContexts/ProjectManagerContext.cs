using Microsoft.EntityFrameworkCore;
using ProjectManager.Entities;

namespace ProjectManager.DbContexts
{
    public class ProjectManagerContext : DbContext
    {
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Project> ProjectObjects { get; set; } = null!;

        public ProjectManagerContext(DbContextOptions<ProjectManagerContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasData(
                new Project("QuarterMaster")
                {
                    Id = 1,
                    Description = "POC project.",
                },
                new Project("LPA")
                {
                    Id = 2,
                    Description = "Ongoing project.",
                });

            modelBuilder.Entity<ProjectObject>().HasData(
                new ProjectObject("QM-323")
                {
                    Id = 1,
                    Description = "Authorization.",
                },
                new ProjectObject("QM-242")
                {
                    Id = 2,
                    Description = "Multi-Level Approval.",
                });

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
