using FBV.DAL.Contracts;
using FBV.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace FBV.DAL.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T: class
    {
        public FBVContext DataContext { get; set; }
        public RepositoryBase(FBVContext dataContext)
        {
            DataContext = dataContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DataContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await DataContext.Set<T>().FindAsync(id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            await DataContext.Set<T>().AddAsync(entity);
            await DataContext.SaveChangesAsync();

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            DataContext.Set<T>().Update(entity);
            await DataContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            DataContext.Set<T>().Remove(entity);
            await DataContext.SaveChangesAsync();
        }
    }
}
