using FBV.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FBV.Domain.Entities
{
    public class PurchaseOrderLine
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public MembershipType MembershipTypeId { get; set; }

        #region Navigation properties

        public int PurchaseOrderId { get; set; }

        [ForeignKey("PurchaseOrderId")]
        public PurchaseOrder PurchaseOrder { get; set; }

        #endregion Navigation properties
    }
}
