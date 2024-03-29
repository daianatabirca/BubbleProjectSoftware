using Comments = ProjectManager.API.Utilities.Comments;

namespace ProjectManager.Services.DTOs
{
    public class ProjectObjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int SprintNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Assignee { get; set; }
        public string? Status { get; set; }
        //public Status? Status { get; set; }
        //public ICollection<ProjectObjectHistory> ProjectObjectHistory { get; set; } = new List<ProjectObjectHistory>();
        //public ICollection<ProjectObjectRelation> ProjectObjectRelations { get; set; } = new List<ProjectObjectRelation>();
        //public ICollection<ProjectObjectRelation> RelatedProjectObjectRelations { get; set; } = new List<ProjectObjectRelation>();
        public ICollection<Comments> Comments { get; set; } = new List<Comments>();
    }
}
