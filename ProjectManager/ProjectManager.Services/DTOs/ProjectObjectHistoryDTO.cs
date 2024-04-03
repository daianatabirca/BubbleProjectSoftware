namespace ProjectManager.Services.DTOs
{
    public class ProjectObjectHistoryDTO
    {
        public string? CreatedBy { get; set; } = string.Empty;

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; } = string.Empty;

        public DateTime? UpdatedDate { get; set; }

        public string? Description { get; set; }

        public int ProjectObjectId { get; set; }
    }
}
