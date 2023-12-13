using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.Entities
{
    public class Comments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? Username { get; set; } //email = username

        [Required]
        public DateTime InsertDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [MaxLength(500)]
        public string? CommentArea { get; set; }
    }
}
