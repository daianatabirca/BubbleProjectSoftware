namespace ProjectManager.DomainModel.Models.Requests
{
    public class CommentRequestPatch
    {
        public string? Username { get; set; } //email = username

        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public string? CommentArea { get; set; }

        public int ProjectObjectId { get; set; }
    }
}
