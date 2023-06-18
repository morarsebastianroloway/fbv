using Microsoft.VisualStudio.TestTools.UnitTesting;
using FBV.API.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FBV.DAL.Contracts;
using FBV.Domain.Entities;
using FBV.Domain.Enums;
using Moq;
using Microsoft.VisualStudio.Web.CodeGeneration;

namespace FBV.API.Managers.Tests
{
    [TestClass]
    public class PurchaseOrderProcessorTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private IPurchaseOrderProcessor _purchaseOrderProcessor;
        private Mock<IFileWrapper> _fileWrapperMock;

        [TestInitialize]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _fileWrapperMock = new Mock<IFileWrapper>();
            _purchaseOrderProcessor = new PurchaseOrderProcessor(_unitOfWorkMock.Object, _customerRepositoryMock.Object, _fileWrapperMock.Object);
        }

        [TestMethod]
        public async Task ProcessNewOrderAsync_ShouldCalculateTotalPrice()
        {
            // Arrange: Set up any required dependencies or test data
            var purchaseOrder = new PurchaseOrder
            {
                // Set the necessary properties of the purchase order
                CustomerId = 1,
                Lines = new List<PurchaseOrderLine>
                {
                    new PurchaseOrderLine { Price = 10 },
                    new PurchaseOrderLine { Price = 20 }
                }
            };

            // Mock the CreateAsync method to return the same object
            _unitOfWorkMock.Setup(uow => uow.PurchaseOrderRepository.CreateAsync(purchaseOrder))
               .ReturnsAsync(purchaseOrder);

            // Act: Invoke the ProcessNewOrderAsync method
            var result = await _purchaseOrderProcessor.ProcessNewOrderAsync(purchaseOrder);

            // Assert: Verify the expected outcome and make assertions
            Assert.AreEqual(30, result.TotalPrice);
        }

        [TestMethod]
        public async Task ProcessNewOrderAsync_ShouldNotChangeMembership()
        {
            // Arrange: Set up any required dependencies or test data
            var purchaseOrder = new PurchaseOrder
            {
                // Set the necessary properties of the purchase order
                CustomerId = 1,
                Lines = new List<PurchaseOrderLine>
                {
                    new PurchaseOrderLine { MembershipTypeId = MembershipType.BookClub }
                }
            };

            // Mock the GetAllByCustomerAsync method to return existing memberships for the customer
            _unitOfWorkMock.Setup(uow => uow.MembershipRepository.GetAllByCustomerAsync(purchaseOrder.CustomerId))
                .ReturnsAsync(new List<Membership>
                {
                    new Membership { MembershipTypeId = MembershipType.BookClub }
                });

            // Mock the CreateAsync method to return the same object
            _unitOfWorkMock.Setup(uow => uow.PurchaseOrderRepository.CreateAsync(purchaseOrder))
               .ReturnsAsync(purchaseOrder);

            // Act: Invoke the ProcessNewOrderAsync method
            var result = await _purchaseOrderProcessor.ProcessNewOrderAsync(purchaseOrder);

            // Assert: Verify the expected outcome and make assertions
            _unitOfWorkMock.Verify(uow => uow.MembershipRepository.CreateAsync(It.IsAny<Membership>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.PurchaseOrderRepository.CreateAsync(It.IsAny<PurchaseOrder>()), Times.Once);
        }

        [TestMethod]
        public async Task ProcessNewOrderAsync_ShouldActivateNewMembership()
        {
            // Arrange: Set up any required dependencies or test data
            var purchaseOrder = new PurchaseOrder
            {
                // Set the necessary properties of the purchase order
                CustomerId = 1,
                Lines = new List<PurchaseOrderLine>
            {
                new PurchaseOrderLine { MembershipTypeId = MembershipType.Premium }
            }
            };

            // Mock the GetAllByCustomerAsync method to return no existing memberships for the customer
            _unitOfWorkMock.Setup(uow => uow.MembershipRepository.GetAllByCustomerAsync(purchaseOrder.CustomerId))
                .ReturnsAsync(new List<Membership>());

            // Mock the CreateAsync method to return the same object
            _unitOfWorkMock.Setup(uow => uow.PurchaseOrderRepository.CreateAsync(purchaseOrder))
               .ReturnsAsync(purchaseOrder);

            // Act: Invoke the ProcessNewOrderAsync method
            var result = await _purchaseOrderProcessor.ProcessNewOrderAsync(purchaseOrder);

            // Assert: Verify the expected outcome and make assertions
            _unitOfWorkMock.Verify(uow => uow.MembershipRepository.CreateAsync(It.IsAny<Membership>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.PurchaseOrderRepository.CreateAsync(It.IsAny<PurchaseOrder>()), Times.Once);
        }

        [TestMethod]
        public async Task ProcessNewOrderAsync_ShouldGenerateShippingSlip()
        {
            // Arrange: Set up any required dependencies or test data
            var purchaseOrder = new PurchaseOrder
            {
                // Set the necessary properties of the purchase order
                CustomerId = 1,
                Lines = new List<PurchaseOrderLine>
            {
                new PurchaseOrderLine { IsPhysical = true }
            }
            };

            var customer = new Customer
            {
                EmailAddress = "test@example.com",
                Address = "123 Test Street"
            };

            // Mock the GetByIdAsync method to return the customer
            _customerRepositoryMock.Setup(repo => repo.GetByIdAsync(purchaseOrder.CustomerId))
                .ReturnsAsync(customer);

            // Mock the CreateAsync method to return the same object
            _unitOfWorkMock.Setup(uow => uow.PurchaseOrderRepository.CreateAsync(purchaseOrder))
               .ReturnsAsync(purchaseOrder);

            // Mock the ReadAllText method that retrieves the template
            _fileWrapperMock.Setup(f => f.ReadAllText(It.IsAny<string>()))
                .Returns(@"<!DOCTYPE html><html><body><h1>Shipping Slip</h1>Mocked template content: {{customerName}}</body></html>");

            // Act: Invoke the ProcessNewOrderAsync method
            var result = await _purchaseOrderProcessor.ProcessNewOrderAsync(purchaseOrder);

            // Assert: Verify the expected outcome and make assertions
            _unitOfWorkMock.Verify(uow => uow.PurchaseOrderRepository.CreateAsync(It.IsAny<PurchaseOrder>()), Times.Once);
            Assert.IsNotNull(result.ShippingSlip);
        }
    }
}