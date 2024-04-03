using Microsoft.EntityFrameworkCore;
using ProjectManager.Repository.Entities;

namespace ProjectManager.DbContexts
{
    public class ProjectManagerContext : DbContext
    {
        public DbSet<Project> Project { get; set; } = null!;
        public DbSet<Status> Status { get; set; } = null!;
        public DbSet<Comment> Comment { get; set; } = null!;
        public DbSet<RelationType> RelationType { get; set; } = null!;
        public DbSet<ProjectObjectType> ProjectObjectType { get; set; } = null!;
        public DbSet<ProjectObject> ProjectObject { get; set; } = null!;
        public DbSet<ProjectObjectHistory> ProjectObjectHistory { get; set; } = null!;
        public DbSet<ProjectObjectRelation> ProjectObjectRelation { get; set; } = null!;

        public ProjectManagerContext(DbContextOptions<ProjectManagerContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectObjectRelation>()
                .HasOne(r => r.ProjectObject)
                .WithMany(p => p.ProjectObjectRelations)
                .HasForeignKey(r => r.ProjectObjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectObjectRelation>()
                .HasOne(r => r.RelatedObject)
                .WithMany(p => p.RelatedProjectObjectRelations)
                .HasForeignKey(r => r.RelatedObjectId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<ProjectObject>()
                .HasOne(po => po.Project)
                .WithMany(p => p.ProjectObjects)
                .HasForeignKey(po => po.ProjectId);

            modelBuilder.Entity<ProjectObjectRelation>()
                .HasIndex(por => new { por.RelationTypeId, por.ProjectObjectId, por.RelatedObjectId })
                .IsUnique()
                .HasDatabaseName("UniqueConstraintRelation_Index");

            //modelBuilder.Entity<Comments>()
            //    .HasOne<ProjectObject>()
            //    .WithMany(po => po.Comments)
            //    .IsRequired();

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ProjectObject)
                .WithMany(po => po.Comments)
                .HasForeignKey(c => c.ProjectObjectId)
                .IsRequired();

            //modelBuilder.Entity<ProjectObject>()
            //    .HasOne<Project>()
            //    .WithMany(p => p.ProjectObjects)
            //    .IsRequired();

            //Seed Database
            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Type = "To Do" },
                new Status { Id = 2, Type = "In Progress" },
                new Status { Id = 3, Type = "Closed" },
                new Status { Id = 4, Type = "Abandoned" });

            modelBuilder.Entity<RelationType>().HasData(
                new RelationType { Id = 1, Type = "Related" },
                new RelationType { Id = 2, Type = "Parent" },
                new RelationType { Id = 3, Type = "Child" });

            modelBuilder.Entity<ProjectObjectType>().HasData(
                new ProjectObjectType { Id = 1, Type = "Epic" },
                new ProjectObjectType { Id = 2, Type = "Feature" },
                new ProjectObjectType { Id = 3, Type = "Story" },
                new ProjectObjectType { Id = 4, Type = "Task" },
                new ProjectObjectType { Id = 5, Type = "Bug" });
        }
    }
}