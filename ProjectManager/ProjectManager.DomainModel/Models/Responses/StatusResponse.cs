namespace ProjectManager.DomainModel.Models.Responses
{
    public class StatusResponse
    {
        public int Id { get; set; }

        public string Type { get; set; } //To Do, In Progress, Closed, Abandoned

        public ICollection<ProjectObjectSummaryResponse> ProjectObjects { get; set; } = new List<ProjectObjectSummaryResponse>();
    }
}
