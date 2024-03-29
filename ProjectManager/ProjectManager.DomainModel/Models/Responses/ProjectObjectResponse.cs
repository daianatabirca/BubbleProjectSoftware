namespace ProjectManager.DomainModel.Models.Responses
{
    public class ProjectObjectResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public int SprintNumber { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? CreatedBy { get; set; } //email = username

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; } //email

        public string? Assignee { get; set; } //email

        public int StatusId { get; set; }

        //public StatusResponse? Status { get; set; }

        public int ProjectObjectTypeId { get; set; }

        public int ProjectId { get; set; }

        //public ProjectObjectTypeResponse? ProjectObjectType { get; set; }

        //public ICollection<ProjectObjectHistory> ProjectObjectHistory { get; set; } = new List<ProjectObjectHistory>();

        //public ICollection<ProjectObjectRelation> ProjectObjectRelations { get; set; } = new List<ProjectObjectRelation>();

        //public ICollection<ProjectObjectRelation> RelatedProjectObjectRelations { get; set; } = new List<ProjectObjectRelation>();

        //public ICollection<Comments> Comments { get; set; } = new List<Comments>();
    }
}
