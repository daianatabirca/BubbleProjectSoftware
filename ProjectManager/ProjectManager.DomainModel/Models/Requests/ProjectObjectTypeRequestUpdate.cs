using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.DomainModel.Models.Requests
{
    public class ProjectObjectTypeRequestUpdate
    {
        public string Type { get; set; } //Epic, Feature, Story, Task, Bug

        //public ICollection<ProjectObject> ProjectObjects { get; set; } = new List<ProjectObject>();
    }
}
