using Microsoft.EntityFrameworkCore;
using ProjectManager.DbContexts;
using ProjectManager.Repository.Entities;

namespace ProjectManager.Repository.Repositories
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
            return await _context.Project.OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<bool> ProjectExistsAsync(int projectId)
        {
            return await _context.Project.AnyAsync(p => p.Id == projectId); //determines whether the list contains elements
        }

        public void AddProject(Project project)
        {
            _context.Project.Add(project);
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException)
            {
                // validations to do if unique key is violated
                return false;
            }
        }

        public async Task<Project?> GetProjectByIdAsync(int projectId)
        {
            return await _context.Project.Where(p => p.Id == projectId).FirstOrDefaultAsync();
        }

        public async Task<Project> UpdateAsync(Project project)
        {
            _context.Project.Update(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task DeleteAsync(Project project)
        {
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
        }

        public bool ProjectExists(int projectId)
        {
            return _context.Project.Any(p => p.Id == projectId);
        }
    }
}
