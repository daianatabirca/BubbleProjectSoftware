namespace ProjectManager.DomainModel.Models.Requests
{
    public class StatusRequest
    {
        //public int Id { get; set; }

        public string Type { get; set; } = string.Empty; //To Do, In Progress, Closed, Abandoned

        //public ICollection<ProjectObjectSummaryResponse> ProjectObjects { get; set; } = new List<ProjectObjectSummaryResponse>();
    }
}
