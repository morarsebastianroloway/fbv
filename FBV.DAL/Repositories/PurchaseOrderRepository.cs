using FBV.DAL.Contracts;
using FBV.DAL.Data;
using FBV.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FBV.DAL.Repositories
{
    public class PurchaseOrderRepository : RepositoryBase<PurchaseOrder>, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository(FBVContext dataContext)
            : base(dataContext)
        {
        }

        public async new Task<IEnumerable<PurchaseOrder>> GetAllAsync()
        {
            return await _dataContext.Set<PurchaseOrder>().Include(po => po.Lines).AsNoTracking().ToListAsync();
        }

        public async new Task<PurchaseOrder?> GetByIdAsync(int id)
        {
            return await _dataContext.Set<PurchaseOrder>().Include(po => po.Lines).FirstOrDefaultAsync(po => po.Id == id);
        }
    }
}
