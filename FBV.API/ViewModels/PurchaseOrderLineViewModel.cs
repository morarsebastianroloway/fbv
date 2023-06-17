using FBV.Domain.Enums;

namespace FBV.API.ViewModels
{
    public class PurchaseOrderLineViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public MembershipType MembershipTypeId { get; set; }

        public int PurchaseOrderId { get; set; }
    }
}
