using FBV.Domain.Enums;

namespace FBV.Domain.Entities
{
    public class PurchaseOrderLine
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public MembershipType MembershipTypeId { get; set; }

        public int PurchaseOrderId { get; set; }
    }
}
