using System.ComponentModel.DataAnnotations.Schema;

namespace FBV.Domain.Entities
{
    public class PurchaseOrder
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        #region Navigation properties

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public List<PurchaseOrderLine> Lines { get; set; }

        #endregion Navigation properties
    }

}
