using Microsoft.EntityFrameworkCore;
using ProjectManager.DbContexts;
using ProjectManager.Repository.Entities;

namespace ProjectManager.Repository.Repositories
{
    public class ProjectObjectRelationRepository : IProjectObjectRelationRepository
    {
        private readonly ProjectManagerContext _context;

        public ProjectObjectRelationRepository(ProjectManagerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddProjectObjectRelation(ProjectObjectRelation projectObjectRelation)
        {
            _context.ProjectObjectRelation.Add(projectObjectRelation);
        }

        public async Task DeleteAsync(ProjectObjectRelation projectObjectRelation)
        {
            _context.ProjectObjectRelation.Remove(projectObjectRelation);
            await _context.SaveChangesAsync();
        }

        public async Task<ProjectObjectRelation?> GetProjectObjectRelationByIdAsync(int projectObjectRelationId)
        {
            return await _context.ProjectObjectRelation.Where(po => po.Id == projectObjectRelationId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProjectObjectRelation>> GetProjectObjectRelationsAsync()
        {
            return await _context.ProjectObjectRelation.OrderBy(po => po.Id).ToListAsync();
        }

        public async Task<bool> ProjectObjectRelationExistsAsync(int projectObjectRelationId)
        {
            return await _context.ProjectObjectRelation.AnyAsync(po => po.Id == projectObjectRelationId);
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

        public async Task<ProjectObjectRelation> UpdateAsync(ProjectObjectRelation projectObjectRelation)
        {
            _context.ProjectObjectRelation.Update(projectObjectRelation);
            await _context.SaveChangesAsync();
            return projectObjectRelation;
        }
    }
}
