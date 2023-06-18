using FBV.Domain.Entities;

namespace FBV.API.Managers
{
    public interface IPurchaseOrderProcessor
    {
        Task<PurchaseOrder> ProcessNewOrderAsync(PurchaseOrder purchaseOrder);
    }
}
