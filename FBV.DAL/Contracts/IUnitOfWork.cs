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
