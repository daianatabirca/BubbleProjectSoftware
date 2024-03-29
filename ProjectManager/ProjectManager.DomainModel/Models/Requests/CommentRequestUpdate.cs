namespace ProjectManager.DomainModel.Models.Requests
{
    public class CommentRequestUpdate
    {
        public string? Username { get; set; } //email = username

        public DateTime UpdatedDate { get; set; }

        public string? CommentArea { get; set; }

        public int ProjectObjectId { get; set; }
    }
}
