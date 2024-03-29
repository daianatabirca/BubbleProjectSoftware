﻿namespace ProjectManager.Services.DTOs
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int NumberOfProjectObjects
        {
            get
            {
                return ProjectObjects.Count;
            }
        }

        public ICollection<ProjectObjectSummaryDto> ProjectObjects { get; set; } = new List<ProjectObjectSummaryDto>();
    }
}
