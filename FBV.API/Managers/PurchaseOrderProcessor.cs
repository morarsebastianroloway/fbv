using FBV.DAL.Contracts;
using FBV.Domain.Entities;

namespace FBV.API.Managers
{
    public class PurchaseOrderProcessor : IPurchaseOrderProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseOrderProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PurchaseOrder> CreatePurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            // Calculate the total price from PO lines and activate memberships if necessary
            purchaseOrder.TotalPrice = 0;
            foreach (var line in purchaseOrder.Lines)
            {
                // Add the price to the total
                purchaseOrder.TotalPrice += line.Price;

                // TODO: Check if the line is a membership and activate it

                // TODO: Check if the line is a physical product and generate shipping slip

            }

            // Add the PO in the database
            var result = await _unitOfWork.PurchaseOrderRepository.CreateAsync(purchaseOrder);

            // Save all the changes we did
            await _unitOfWork.SaveAsync();

            return result;
        }
    }
}
