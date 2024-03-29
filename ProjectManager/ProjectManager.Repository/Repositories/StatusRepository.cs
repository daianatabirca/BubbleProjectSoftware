using Microsoft.EntityFrameworkCore;
using ProjectManager.DbContexts;
using ProjectManager.Repository.Entities;

namespace ProjectManager.Repository.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly ProjectManagerContext _context;

        public StatusRepository(ProjectManagerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddStatus(Status status)
        {
            _context.Status.Add(status);
        }

        public async Task DeleteAsync(Status status)
        {
            _context.Status.Remove(status);
            await _context.SaveChangesAsync();
        }

        public async Task<Status?> GetStatusByIdAsync(int statusId)
        {
            return await _context.Status.Where(s => s.Id == statusId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Status>> GetStatusesAsync()
        {
            return await _context.Status.OrderBy(s => s.Type).ToListAsync();
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

        public bool StatusExists(int statusId)
        {
            return _context.Status.Any(s => s.Id == statusId);
        }

        public async Task<bool> StatusExistsAsync(int statusId)
        {
            return await _context.Status.AnyAsync(s => s.Id == statusId); //determines whether the list contains elements
        }

        public async Task<Status> UpdateAsync(Status status)
        {
            _context.Status.Update(status);
            await _context.SaveChangesAsync();
            return status;
        }
    }
}
