using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FBV.Domain.Entities
{
    public class PurchaseOrder
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public byte[] ShippingSlip { get; set; } = default!;

        #region Navigation properties

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; } = default!;

        public List<PurchaseOrderLine> Lines { get; set; } = default!;

        #endregion Navigation properties
    }
}
