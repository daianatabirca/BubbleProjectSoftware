namespace ProjectManager.DomainModel.Models.Requests
{
    public class ProjectObjectRequest
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public int SprintNumber { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? CreatedBy { get; set; } //email = username

        public string? Assignee { get; set; } //email

        public int StatusId { get; set; } = (int)Enums.Status.ToDo;

        //public StatusRequest? Status { get; set; }

        public int ProjectObjectTypeId { get; set; }

        //public ProjectObjectTypeRequest? ProjectObjectType { get; set; }

        //public ProjectRequest Project {  get; set; }
        public int ProjectId {  get; set; }

        //public ICollection<ProjectObjectHistory> ProjectObjectHistory { get; set; } = new List<ProjectObjectHistory>();

        //public ICollection<ProjectObjectRelation> ProjectObjectRelations { get; set; } = new List<ProjectObjectRelation>();

        //public ICollection<ProjectObjectRelation> RelatedProjectObjectRelations { get; set; } = new List<ProjectObjectRelation>();

        //public ICollection<Comments> Comments { get; set; } = new List<Comments>();
    }
}
