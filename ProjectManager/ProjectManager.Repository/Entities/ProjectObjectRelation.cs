using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ProjectManager.Entities
{
    public class ProjectObjectRelation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int RelationTypeId { get; set; }

        [ForeignKey("RelationTypeId")]
        public RelationType? RelationType { get; set; }

        [Required]
        public int ProjectObjectId { get; set; }

        [ForeignKey("ProjectObjectId")]
        public ProjectObject? ProjectObject { get; set; }

        [Required]
        public int RelatedObjectId { get; set; }

        [ForeignKey("RelatedObjectId")]
        public ProjectObject? RelatedObject { get; set; }
    }
}
