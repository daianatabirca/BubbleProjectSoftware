﻿namespace ProjectManager.Services.DTOs
{
    public class ProjectWithoutProjectObjectsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
