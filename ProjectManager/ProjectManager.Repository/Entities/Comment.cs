using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.Repository.Entities
{
    public class Comment
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

        public int ProjectObjectId { get; set; }

        public ProjectObject ProjectObject { get; set; }
    }
}
