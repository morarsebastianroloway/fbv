using FBV.DAL.Contracts;
using FBV.DAL.Data;
using FBV.Domain.Entities;

namespace FBV.DAL.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(FBVContext dataContext)
            : base(dataContext)
        {
        }
    }
}
