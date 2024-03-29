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
    public class CommentRepository : ICommentRepository
    {
        private readonly ProjectManagerContext _context;

        public CommentRepository(ProjectManagerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddComment(Comment comments)
        {
            _context.Comment.Add(comments);
        }

        public async Task<bool> CommentExistsAsync(int commentsId)
        {
            return await _context.Comment.AnyAsync(c => c.Id == commentsId);
        }

        public async Task DeleteAsync(Comment comments)
        {
            _context.Comment.Remove(comments);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync()
        {
            return await _context.Comment.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(int commentsId)
        {
            return await _context.Comment.Where(c => c.Id == commentsId).FirstOrDefaultAsync();
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

        public async Task<Comment> UpdateAsync(Comment comments)
        {
            _context.Comment.Update(comments);
            await _context.SaveChangesAsync();
            return comments;
        }
    }
}
