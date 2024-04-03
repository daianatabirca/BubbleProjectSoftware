namespace ProjectManager.DomainModel.Models.Responses
{
    public class ProjectObjectRelationResponse
    {
        public int Id { get; set; }

        public int RelationTypeId { get; set; }

        public int ProjectObjectId { get; set; }

        public int RelatedObjectId { get; set; }
    }
}
