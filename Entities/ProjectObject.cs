using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Entities
{
    public class ProjectObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public int SprintNumber { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public string? CreatedBy { get; set; } //email = username

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; } //email

        public string? Assignee { get; set; } //email

        [Required]
        public int StatusId { get; set; }

        //to do = default
        [ForeignKey("StatusId")]
        public Status? Status { get; set; }

        public ICollection<ProjectObjectHistory> ProjectObjectHistory { get; set; } = new List<ProjectObjectHistory>();

        public ICollection<ProjectObjectRelation> ProjectObjectRelations { get; set; } = new List<ProjectObjectRelation>();

        public ICollection<ProjectObjectRelation> RelatedProjectObjectRelations { get; set; } = new List<ProjectObjectRelation>();

        public ICollection<Comments> Comments { get; set; } = new List<Comments>();

        public ProjectObject(string name)
        {
            Name = name;
        }
    }
}
