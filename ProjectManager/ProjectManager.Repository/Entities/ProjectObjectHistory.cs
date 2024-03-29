using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Repository.Entities
{
    public class ProjectObjectHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string? UpdatedBy { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [Required]
        public int ProjectObjectId { get; set; }

        [ForeignKey("ProjectObjectId")]
        public ProjectObject? ProjectObject { get; set; }
    }
}
