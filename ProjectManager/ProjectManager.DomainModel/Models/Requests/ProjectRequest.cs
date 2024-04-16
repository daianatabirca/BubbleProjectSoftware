using System.ComponentModel.DataAnnotations;

namespace ProjectManager.DomainModel.Models.Requests
{
    public class ProjectRequest
    {
        //TODO: Add data adnotations for every request
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        //public ICollection<ProjectObjectForCreationDto> ProjectObjects { get; set; } = new List<ProjectObjectForCreationDto>();
    }
}
