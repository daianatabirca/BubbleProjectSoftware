using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Entities
{
    public class Comments
    {
        //insertDate
        //updateDate
        //name
        //commentArea

        [Required]
        public string? Username { get; set; } //email = username

        [Required]
        public DateTime InsertDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [MaxLength(500)]
        public string? CommentArea { get; set; }
    }
}
