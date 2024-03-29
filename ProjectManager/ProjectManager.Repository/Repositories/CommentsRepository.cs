using Microsoft.EntityFrameworkCore;
using ProjectManager.DbContexts;
using ProjectManager.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Repository.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ProjectManagerContext _context;

        public CommentsRepository(ProjectManagerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddComments(Comments comments)
        {
            _context.Comments.Add(comments);
        }

        public async Task<bool> CommentsExistsAsync(int commentsId)
        {
            return await _context.Comments.AnyAsync(c => c.Id == commentsId);
        }

        public async Task DeleteAsync(Comments comments)
        {
            _context.Comments.Remove(comments);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comments>> GetCommentsAsync()
        {
            return await _context.Comments.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<Comments?> GetCommentsByIdAsync(int commentsId)
        {
            return await _context.Comments.Where(c => c.Id == commentsId).FirstOrDefaultAsync();
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

        public async Task<Comments> UpdateAsync(Comments comments)
        {
            _context.Comments.Update(comments);
            await _context.SaveChangesAsync();
            return comments;
        }
    }
}
