namespace ProjectManager.DomainModel.Models.Requests
{
    public class ProjectObjectTypeRequest
    {
        public string Type { get; set; } //Epic, Feature, Story, Task, Bug

        //public ICollection<ProjectObject> ProjectObjects { get; set; } = new List<ProjectObject>();
    }
}
