namespace ProjectManager.DomainModel.Models.Requests
{
    public class ProjectObjectRelationRequestUpdate
    {
        //public int Id { get; set; }

        public int RelationTypeId { get; set; }

        public int ProjectObjectId { get; set; }

        public int RelatedObjectId { get; set; }
    }
}
