using FBV.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBV.DAL.Contracts
{
    public interface IUnitOfWork
    {
        IRepositoryBase<PurchaseOrder> PurchaseOrderRepository { get;}
        IRepositoryBase<PurchaseOrderLine> PurchaseOrderLineRepository { get; }

        Task SaveAsync();
    }
}
