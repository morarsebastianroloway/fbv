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
    public class PurchaseOrderLineRepository : RepositoryBase<PurchaseOrderLine>, IPurchaseOrderLineRepository
    {
        public PurchaseOrderLineRepository(FBVContext dataContext)
            : base(dataContext)
        {
        }
    }
}
