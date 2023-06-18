using FBV.DAL.Contracts;
using FBV.DAL.Data;
using FBV.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBV.DAL.Repositories
{
    public class MembershipRepository : RepositoryBase<Membership>, IMembershipRepository
    {
        public MembershipRepository(FBVContext dataContext)
            : base(dataContext)
        {
        }

        public async Task<IEnumerable<Membership>> GetAllByCustomerAsync(int customerId)
        {
            return await _dataContext.Set<Membership>().Where(m => m.CustomerId == customerId).AsNoTracking().ToListAsync();
        }
    }
}
