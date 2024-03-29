namespace ProjectManager.DomainModel.Models.Requests
{
    public class ProjectRequestUpdate
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
