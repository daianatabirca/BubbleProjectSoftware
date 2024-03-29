namespace ProjectManager.DomainModel.Models.Responses
{
    public class RelationTypeResponse
    {
        public int Id { get; set; }

        public string Type { get; set; } //Related, Child, Parent

        public ICollection<ProjectObjectSummaryResponse> ProjectObjects { get; set; } = new List<ProjectObjectSummaryResponse>();
    }
}
