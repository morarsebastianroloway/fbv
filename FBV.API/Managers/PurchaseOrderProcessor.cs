using FBV.DAL.Contracts;
using FBV.Domain.Entities;
using FBV.Domain.Enums;
using HandlebarsDotNet;
using PdfSharp;
using PdfSharp.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace FBV.API.Managers
{
    public class PurchaseOrderProcessor : IPurchaseOrderProcessor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;

        public PurchaseOrderProcessor(IUnitOfWork unitOfWork, ICustomerRepository customerRepository)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
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
            }

            // Check if at least on line is a physical product and generate shipping slip. Since there aren't any
            // details, we assume all the physical products are going to be shipped in the same time. So the shipping
            // slip will be saved to the POs table
            var physicalProducts = purchaseOrder.Lines.Where(pol => pol.MembershipTypeId == MembershipType.None && pol.IsPhysical);
            if (physicalProducts != null && physicalProducts.Any())
            {
                var customer = await _customerRepository.GetByIdAsync(purchaseOrder.CustomerId);
                if (customer != null)
                {
                    purchaseOrder.ShippingSlip = GenerateShippingSlip(customer, physicalProducts);
                }
            }

            // Add the PO in the database
            var result = await _unitOfWork.PurchaseOrderRepository.CreateAsync(purchaseOrder);

            // Save all the changes we did
            await _unitOfWork.SaveAsync();

            return result;
        }

        private byte[] GenerateShippingSlip(Customer customer, IEnumerable<PurchaseOrderLine> lines)
        {
            // Load the template from the file
            string templatePath = "shippingSlip.html";
            string templateContent = File.ReadAllText(templatePath);

            // Prepare the data to populate the template
            var data = new
            {
                customerName = customer.EmailAddress,
                customerAddress = customer.Address,
                products = lines
            };

            // Compile and render the template with the data
            string renderedTemplate = Handlebars.Compile(templateContent)(data);

            PdfDocument pdfDocument = PdfGenerator.GeneratePdf(renderedTemplate, PageSize.A4);

            byte[] fileContents;
            using (MemoryStream stream = new MemoryStream())
            {
                pdfDocument.Save(stream, true);
                fileContents = stream.ToArray();
            }

            return fileContents;
        }
    }
}
