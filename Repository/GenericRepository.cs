using EcWebapi.Database;
using EcWebapi.Database.Table;
using EcWebapi.Dto;
using EcWebapi.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EcWebapi.Repository
{
    public class GenericRepository<T>(EcDbContext context, PayloadService payloadService) where T : Entity
    {
        private readonly EcDbContext _context = context;

        private readonly PayloadDto _payload = payloadService.GetPayload();

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

            entity.CreateBy = _payload.Id.Value;

            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            entity.ModificationTime = DateTime.Now;
            if (_payload.Id != null) entity.ModifyBy = _payload.Id.Value;
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

        public IQueryable<T> GetQuerable(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetQuerable()
        {
            return _context.Set<T>();
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