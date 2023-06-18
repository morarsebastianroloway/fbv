using FBV.Domain.Entities;

namespace FBV.DAL.Contracts
{
    public interface IMembershipRepository : IRepositoryBase<Membership>
    {
        Task<IEnumerable<Membership>> GetAllByCustomerAsync(int customerId);
    }
}
