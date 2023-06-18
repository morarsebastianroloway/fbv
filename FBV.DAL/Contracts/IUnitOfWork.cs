using FBV.DAL.Repositories;
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
        IPurchaseOrderRepository PurchaseOrderRepository { get;}
        IPurchaseOrderLineRepository PurchaseOrderLineRepository { get; }
        IMembershipRepository MembershipRepository { get; }

        Task SaveAsync();
    }
}
