using EcWebapi.Database;
using EcWebapi.Database.Table;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq.Expressions;

namespace EcWebapi.Repository
{
    public class GenericRepository<T>(EcDbContext context) where T : Entity
    {
        private readonly EcDbContext _context = context;

        public async Task<IList<T>> GetListAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(T entity)
        {
            entity.CreationTime = DateTime.Now;
            entity.EntityStatus = true;
            entity.Id = Guid.NewGuid();
            await _context.Set<T>().AddAsync(entity);
            Log.Information(_context.Entry(entity).State.ToString());
            Log.Information(_context.ChangeTracker.DebugView.LongView);
        }

        public void Update(T entity)
        {
            entity.ModificationTime = DateTime.Now;
        }

        public void Delete(T entity)
        {
            entity.EntityStatus = false;
            entity.DeletionTime = DateTime.Now;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id);
            if (entity != null) Delete(entity);
        }

        public void HardDelete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task HardDeleteById(Guid id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id);
            if (entity != null) HardDelete(entity);
        }

        /// <summary>
        /// 儲存異動。
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}