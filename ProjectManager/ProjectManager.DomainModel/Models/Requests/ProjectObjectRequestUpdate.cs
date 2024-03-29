namespace ProjectManager.DomainModel.Models.Requests
{
    public class ProjectObjectRequestUpdate
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public int SprintNumber { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; } //email

        public string? Assignee { get; set; } //email

        public int StatusId { get; set; }

        //public StatusRequest? Status { get; set; }

        public int ProjectObjectTypeId { get; set; }

        public int ProjectId { get; set; }

        //public ProjectObjectTypeRequest? ProjectObjectType { get; set; }

        //public ICollection<ProjectObjectHistory> ProjectObjectHistory { get; set; } = new List<ProjectObjectHistory>();

        //public ICollection<ProjectObjectRelation> ProjectObjectRelations { get; set; } = new List<ProjectObjectRelation>();

        //public ICollection<ProjectObjectRelation> RelatedProjectObjectRelations { get; set; } = new List<ProjectObjectRelation>();

        //public ICollection<Comments> Comments { get; set; } = new List<Comments>();
    }
}
