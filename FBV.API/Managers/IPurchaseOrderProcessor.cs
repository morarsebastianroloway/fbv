using FBV.Domain.Entities;

namespace FBV.API.Managers
{
    public interface IPurchaseOrderProcessor
    {
        Task<PurchaseOrder> CreatePurchaseOrderAsync(PurchaseOrder purchaseOrder);
    }
}
