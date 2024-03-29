namespace ProjectManager.Services.DTOs
{
    public class ProjectObjectSummaryDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
