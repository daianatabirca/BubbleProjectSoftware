using Microsoft.EntityFrameworkCore;
using ProjectManager.DbContexts;
using ProjectManager.Repository.Entities;

namespace ProjectManager.Repository.Repositories
{
    public class RelationTypeRepository : IRelationTypeRepository
    {
        private readonly ProjectManagerContext _context;

        public RelationTypeRepository(ProjectManagerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddRelationType(RelationType relationType)
        {
            _context.RelationType.Add(relationType);
        }

        public async Task DeleteAsync(RelationType relationType)
        {
            _context.RelationType.Remove(relationType);
            await _context.SaveChangesAsync();
        }

        public async Task<RelationType?> GetRelationTypeByIdAsync(int relationTypeId)
        {
            return await _context.RelationType.Where(r => r.Id == relationTypeId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RelationType>> GetRelationTypesAsync()
        {
            return await _context.RelationType.OrderBy(r => r.Type).ToListAsync();
        }

        public async Task<bool> RelationTypeExistsAsync(int relationTypeId)
        {
            return await _context.RelationType.AnyAsync(r => r.Id == relationTypeId); //determines whether the list contains elements
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

        public async Task<RelationType> UpdateAsync(RelationType relationType)
        {
            _context.RelationType.Update(relationType);
            await _context.SaveChangesAsync();
            return relationType;
        }
    }
}
