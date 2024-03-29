using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.API.Utilities
{
    public class Comments
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? CommentArea { get; set; }
    }
}
