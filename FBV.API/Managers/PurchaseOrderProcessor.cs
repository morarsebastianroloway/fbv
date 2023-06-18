using FBV.DAL.Contracts;
using FBV.Domain.Entities;
using FBV.Domain.Enums;

namespace FBV.API.Managers
{
    public class PurchaseOrderProcessor : IPurchaseOrderProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseOrderProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PurchaseOrder> ProcessNewOrderAsync(PurchaseOrder purchaseOrder)
        {
            // Calculate the total price from PO lines and activate memberships if necessary
            purchaseOrder.TotalPrice = 0;
            foreach (var line in purchaseOrder.Lines)
            {
                // Add the price to the total
                purchaseOrder.TotalPrice += line.Price;

                // Check if the line is a membership and activate it
                if (line.MembershipTypeId != MembershipType.None)
                {
                    // Check if the membership already exists and add it only if not
                    var memberships = await _unitOfWork.MembershipRepository.GetAllByCustomerAsync(purchaseOrder.CustomerId);
                    if (memberships.Any(m => m.MembershipTypeId == line.MembershipTypeId))
                    {
                        // Customer already has this membership activated
                        continue;
                    }

                    // Activate the membership by adding it to the memberships table
                    await _unitOfWork.MembershipRepository.CreateAsync(new Membership()
                    {
                        CustomerId = purchaseOrder.CustomerId,
                        MembershipTypeId = line.MembershipTypeId
                    });
                }

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
