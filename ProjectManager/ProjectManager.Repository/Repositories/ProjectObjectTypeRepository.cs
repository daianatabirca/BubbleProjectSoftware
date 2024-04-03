using Microsoft.EntityFrameworkCore;
using ProjectManager.DbContexts;
using ProjectManager.Repository.Entities;

namespace ProjectManager.Repository.Repositories
{
    public class ProjectObjectTypeRepository : IProjectObjectTypeRepository
    {
        private readonly ProjectManagerContext _context;

        public ProjectObjectTypeRepository(ProjectManagerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddProjectObjectType(ProjectObjectType projectObjectType)
        {
            _context.ProjectObjectType.Add(projectObjectType);
        }

        public async Task DeleteAsync(ProjectObjectType projectObjectType)
        {
            _context.ProjectObjectType.Remove(projectObjectType);
            await _context.SaveChangesAsync();
        }

        public async Task<ProjectObjectType?> GetProjectObjectTypeByIdAsync(int projectObjectTypeId)
        {
            return await _context.ProjectObjectType.Where(po => po.Id == projectObjectTypeId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProjectObjectType>> GetProjectObjectTypesAsync()
        {
            return await _context.ProjectObjectType.OrderBy(po => po.Type).ToListAsync();
        }

        public async Task<bool> ProjectObjectTypeExistsAsync(int projectObjectTypeId)
        {
            return await _context.ProjectObjectType.AnyAsync(po => po.Id == projectObjectTypeId);
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

        public async Task<ProjectObjectType> UpdateAsync(ProjectObjectType projectObjectType)
        {
            _context.ProjectObjectType.Update(projectObjectType);
            await _context.SaveChangesAsync();
            return projectObjectType;
        }
    }
}
