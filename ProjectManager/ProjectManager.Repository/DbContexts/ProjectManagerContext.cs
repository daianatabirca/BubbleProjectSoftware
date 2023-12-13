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

            modelBuilder.Entity<ProjectObjectRelation>()
                .HasIndex(por => new { por.RelationTypeId, por.ProjectObjectId, por.RelatedObjectId })
                .IsUnique()
                .HasDatabaseName("UniqueConstraintRelation_Index");

            modelBuilder.Entity<Comments>()
                .HasOne<ProjectObject>()
                .WithMany(po => po.Comments)
                .IsRequired();

            modelBuilder.Entity<ProjectObject>()
                .HasOne<Project>()
                .WithMany(p => p.ProjectObjects)
                .IsRequired();
        }
    }
}