using ProjectManager.Repository.Entities;

namespace ProjectManager.DomainModel.Models.Responses
{
    public class ProjectObjectTypeResponse
    {
        public int Id { get; set; }

        public string Type { get; set; } //Epic, Feature, Story, Task, Bug

        public ICollection<ProjectObjectSummaryResponse> ProjectObjects { get; set; } = new List<ProjectObjectSummaryResponse>();
    }
}
