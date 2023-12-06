using Microsoft.EntityFrameworkCore;
using ProjectManager.DbContexts;
using ProjectManager.Entities;

namespace ProjectManager.Services
{
    public class ProjectManagerRepository : IProjectManagerRepository
    {
        private readonly ProjectManagerContext _context;

        public ProjectManagerRepository(ProjectManagerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            return await _context.Projects.OrderBy(p => p.Name).ToListAsync();
        }
    }
}
