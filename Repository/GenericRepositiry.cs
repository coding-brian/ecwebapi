using EcWebapi.Database;
using EcWebapi.Database.Table;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EcWebapi.Repository
{
    public class GenericRepositiry<T>(EcDbContext context) where T : Entity
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
            entity.Id = Guid.NewGuid();
            entity.CreationTime = DateTime.Now;
            await _context.Set<T>().AddAsync(entity);
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
            _context.Entry<T>(entity).State = EntityState.Deleted;
        }

        public async Task HardDeleteById(Guid id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id);
            if (entity != null) _context.Entry<T>(entity).State = EntityState.Deleted;
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