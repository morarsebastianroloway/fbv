using FBV.DAL.Contracts;
using FBV.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace FBV.DAL.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T: class
    {
        public FBVContext _dataContext { get; set; }
        public RepositoryBase(FBVContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dataContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dataContext.Set<T>().FindAsync(id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dataContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public T Update(T entity)
        {
            _dataContext.Set<T>().Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _dataContext.Set<T>().Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
