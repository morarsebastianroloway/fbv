﻿namespace FBV.Domain.Entities
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }

        public int CustomerId { get; set; }

    }

}
