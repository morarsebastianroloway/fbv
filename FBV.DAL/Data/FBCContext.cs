using FBV.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FBV.DAL.Data
{
    public class FBVContext : DbContext
    {
        public FBVContext()
        {
        }

        public FBVContext(DbContextOptions<FBVContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<PurchaseOrderLine> PurchaseOrderLines { get; set; }
    }
}
