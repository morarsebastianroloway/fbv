using FBV.DAL.Contracts;
using FBV.DAL.Data;
using FBV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBV.DAL.Repositories
{
    public class PurchaseOrderRepository : RepositoryBase<PurchaseOrder>, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository(FBVContext dataContext)
            : base(dataContext)
        {
        }
    }
}
