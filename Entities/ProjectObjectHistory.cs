using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Entities
{
    public class ProjectObjectHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [Required]
        public int ProjectObjectId { get; set; }

        [ForeignKey("ProjectObjectId")]
        public ProjectObject? ProjectObject { get; set; }
    }
}
