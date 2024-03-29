using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Repository.Entities
{
    public class ProjectObjectType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; } //Epic, Feature, Story, Task, Bug

        //public ICollection<ProjectObject> ProjectObjects { get; set; } = new List<ProjectObject>();
    }
}
