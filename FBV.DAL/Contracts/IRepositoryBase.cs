using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBV.DAL.Contracts
{
    public interface IRepositoryBase<T>
    {
        public Task<T> CreateAsync(T _object);
        public void Delete(T _object);
        public T Update(T _object);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T?> GetByIdAsync(int Id);
        public Task SaveChangesAsync();
    }
}
