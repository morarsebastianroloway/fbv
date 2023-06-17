using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBV.DAL.Contracts
{
    public interface IRepository<T>
    {
        public Task<T> CreateAsync(T _object);
        public Task DeleteAsync(T _object);
        public Task<T> UpdateAsync(T _object);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T?> GetByIdAsync(int Id);
    }
}
