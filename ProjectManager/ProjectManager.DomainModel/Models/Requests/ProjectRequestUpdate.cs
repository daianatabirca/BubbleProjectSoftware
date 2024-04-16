using System.ComponentModel.DataAnnotations;

namespace ProjectManager.DomainModel.Models.Requests
{
    public class ProjectRequestUpdate
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}