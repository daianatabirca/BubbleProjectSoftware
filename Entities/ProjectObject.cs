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

        //Relations
        public ICollection<ProjectObjectHistory> ProjectObjectHistory { get; set; } = new List<ProjectObjectHistory>();

        public ICollection<ProjectObjectRelation> Relations { get; set; } = new List<ProjectObjectRelation>();

        public ICollection<Comments> Comments { get; set; } = new List<Comments>();

        public void AddRelation(ProjectObjectRelation relation)
        {
            
        }

        public ProjectObject(string name)
        {
            Name = name;
            Status = new Status { Type = "To Do"};
        }
    }
}
