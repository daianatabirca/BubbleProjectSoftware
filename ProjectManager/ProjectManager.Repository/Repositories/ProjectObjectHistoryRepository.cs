using Microsoft.EntityFrameworkCore;
using ProjectManager.DbContexts;
using ProjectManager.Repository.Entities;

namespace ProjectManager.Repository.Repositories
{
    public class ProjectObjectHistoryRepository : IProjectObjectHistoryRepository
    {
        private readonly ProjectManagerContext _context;

        public ProjectObjectHistoryRepository(ProjectManagerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddProjectObjectHistory(ProjectObjectHistory projectObjectHistory)
        {
            _context.ProjectObjectHistory.Add(projectObjectHistory);
        }

        public async Task DeleteAsync(ProjectObjectHistory projectObjectHistory)
        {
            _context.ProjectObjectHistory.Remove(projectObjectHistory);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProjectObjectHistory>> GetProjectObjectHistoriesAsync()
        {
            return await _context.ProjectObjectHistory.OrderBy(po => po.Id).ToListAsync();
        }

        public async Task<ProjectObjectHistory?> GetProjectObjectHistoryByIdAsync(int projectObjectHistoryId)
        {
            return await _context.ProjectObjectHistory.Where(po => po.Id == projectObjectHistoryId).FirstOrDefaultAsync();
        }

        public async Task<bool> ProjectObjectHistoryExistsAsync(int projectObjectHistoryId)
        {
            return await _context.ProjectObjectHistory.AnyAsync(po => po.Id == projectObjectHistoryId);
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

        public async Task<ProjectObjectHistory> UpdateAsync(ProjectObjectHistory projectObjectHistory)
        {
            _context.ProjectObjectHistory.Update(projectObjectHistory);
            await _context.SaveChangesAsync();
            return projectObjectHistory;
        }
    }
}
