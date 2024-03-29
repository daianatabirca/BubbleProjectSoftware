using Microsoft.EntityFrameworkCore;
using ProjectManager.DbContexts;
using ProjectManager.Repository.Entities;

namespace ProjectManager.Repository.Repositories
{
    public class ProjectObjectRepository : IProjectObjectRepository
    {
        private readonly ProjectManagerContext _context;

        public ProjectObjectRepository(ProjectManagerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddProjectObject(ProjectObject projectObject)
        {
            _context.ProjectObject.Add(projectObject);
        }

        public async Task DeleteAsync(ProjectObject projectObject)
        {
            _context.ProjectObject.Remove(projectObject);
            await _context.SaveChangesAsync();
        }

        public async Task<ProjectObject?> GetProjectObjectByIdAsync(int projectObjectId)
        {
            return await _context.ProjectObject.Where(po => po.Id == projectObjectId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProjectObject>> GetProjectObjectsAsync()
        {
            return await _context.ProjectObject.OrderBy(po => po.Id).ToListAsync();
        }

        public bool ProjectObjectExists(int projectObjectId)
        {
            return _context.ProjectObject.Any(po => po.Id == projectObjectId);
        }

        public async Task<bool> ProjectObjectExistsAsync(int projectObjectId)
        {
            return await _context.ProjectObject.AnyAsync(po => po.Id == projectObjectId);
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

        public async Task<ProjectObject> UpdateAsync(ProjectObject projectObject)
        {
            _context.ProjectObject.Update(projectObject);
            await _context.SaveChangesAsync();
            return projectObject;
        }
    }
}
