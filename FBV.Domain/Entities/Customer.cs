using System.ComponentModel.DataAnnotations;

namespace FBV.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string EmailAddress { get; set; } = string.Empty;

        #region Navigation properties

        public List<PurchaseOrder>? PurchaseOrders { get; set; }
        
        #endregion Navigation properties
    }
}
