using FBV.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
