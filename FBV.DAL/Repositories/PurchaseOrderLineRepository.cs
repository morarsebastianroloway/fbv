using FBV.DAL.Contracts;
using FBV.DAL.Data;
using FBV.Domain.Entities;

namespace FBV.DAL.Repositories
{
    public class PurchaseOrderLineRepository : RepositoryBase<PurchaseOrderLine>, IPurchaseOrderLineRepository
    {
        public PurchaseOrderLineRepository(FBVContext dataContext)
            : base(dataContext)
        {
        }
    }
}
