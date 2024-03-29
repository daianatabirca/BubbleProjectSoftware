namespace ProjectManager.DomainModel.Models.Responses
{
    public class CommentResponse
    {
        public int Id { get; set; }

        public string? Username { get; set; } //email = username

        public DateTime InsertDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? CommentArea { get; set; }

        public int ProjectObjectId { get; set; }
    }
}
