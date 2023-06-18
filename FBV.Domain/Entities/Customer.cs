using System.ComponentModel.DataAnnotations;

namespace FBV.Domain.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(200)]
        public string EmailAddress { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Address { get; set; } = string.Empty;

        #region Navigation properties

        public List<PurchaseOrder>? PurchaseOrders { get; set; }
        
        #endregion Navigation properties
    }
}
