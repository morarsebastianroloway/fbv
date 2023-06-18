using FBV.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FBV.API.ViewModels
{
    public class PurchaseOrderLineViewModel
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; } = string.Empty;

        public decimal Price { get; set; }
        
        public bool IsPhysical { get; set; }
        
        public MembershipType MembershipTypeId { get; set; }

        public int PurchaseOrderId { get; set; }
    }
}
