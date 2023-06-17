namespace FBV.API.ViewModels
{
    public class PurchaseOrderViewModel
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }

        public int CustomerId { get; set; }
    }
}
