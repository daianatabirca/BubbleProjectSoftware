namespace ProjectManager.DomainModel.Models.Requests
{
    public class RelationTypeRequestUpdate
    {
        public string Type { get; set; } //Related, Child, Parent

        //public ICollection<ProjectObject> ProjectObjects { get; set; } = new List<ProjectObject>();
    }
}
