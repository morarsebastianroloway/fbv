using AutoMapper;
using FBV.API.Profiles;
using FBV.API.ViewModels;
using FBV.DAL.Contracts;
using FBV.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FBV.API.Controllers.Tests
{
    [TestClass]
    public class CustomerControllerTests
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerController _customerController;

        public CustomerControllerTests()
        {
            // Set up dependencies
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomerProfile>(); // Assuming you have a mapping profile set up
            }).CreateMapper();

            _customerRepository = new Mock<ICustomerRepository>().Object; // Use a mock or a test implementation of the repository

            // Create an instance of the controller to be tested
            _customerController = new CustomerController(_mapper, _customerRepository);
        }

        [TestMethod]
        public async Task GetAsyncTest()
        {
            // Arrange: Set up any required dependencies or test data
            var customers = new List<Customer>
            {
                new Customer { Id = 1, EmailAddress = "John.Doe@gmail.com" },
                new Customer { Id = 2, EmailAddress = "Jane.Smith@gmail.com" }
            };
            Mock.Get(_customerRepository)
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(customers);

            // Act: Invoke the GetAsync action method
            var result = await _customerController.GetAsync();

            // Assert: Verify the expected outcome and make assertions
            Assert.IsNotNull(result); // Ensure the result is not null

            // Compare the count of returned customers with the expected count
            Assert.AreEqual(customers.Count, result.Count());

            // Compare specific properties or values if needed
            Assert.AreEqual(customers[0].EmailAddress, result.First().EmailAddress);
            Assert.AreEqual(customers[1].EmailAddress, result.Skip(1).First().EmailAddress);
        }

        [TestMethod]
        public async Task GetByIdAsyncTest()
        {
            // Arrange: Set up any required dependencies or test data
            var customerId = 1;
            var customer = new Customer { Id = customerId, EmailAddress = "John.Doe@gmail.com" };
            Mock.Get(_customerRepository)
                .Setup(r => r.GetByIdAsync(customerId))
                .ReturnsAsync(customer);

            // Act: Invoke the Get action method with the specified ID
            var result = await _customerController.Get(customerId);

            // Assert: Verify the expected outcome and make assertions
            Assert.IsNotNull(result); // Ensure the result is not null

            // Compare the properties or values of the returned customer with the expected values
            Assert.AreEqual(customer.EmailAddress, result.EmailAddress);
        }

        [TestMethod]
        public async Task PostAsyncTest()
        {
            // Arrange: Set up any required dependencies or test data
            var customerViewModel = new CustomerViewModel
            {
                EmailAddress = "test@example.com"
            };

            var createdCustomer = new Customer
            {
                Id = 1,
                EmailAddress = customerViewModel.EmailAddress
            };

            Mock.Get(_customerRepository)
                .Setup(r => r.CreateAsync(It.IsAny<Customer>()))
                .ReturnsAsync(createdCustomer);

            // Act: Invoke the PostAsync action method with the customer view model
            var result = await _customerController.Post(customerViewModel);

            // Assert: Verify the expected outcome and make assertions
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            var createdAtActionResult = (CreatedAtActionResult)result;
            Assert.AreEqual("Get", createdAtActionResult.ActionName); // Ensure it redirects to the "Get" action
            Assert.AreEqual(createdCustomer.Id, createdAtActionResult.RouteValues["id"]); // Ensure the ID is correct
        }

        [TestMethod]
        public async Task PutAsyncTest()
        {
            // Arrange: Set up any required dependencies or test data
            int customerId = 1;
            var customerViewModel = new CustomerViewModel
            {
                Id = customerId,
                EmailAddress = "updated@example.com"
            };

            var existingCustomer = new Customer
            {
                Id = customerId,
                EmailAddress = "original@example.com"
            };

            Mock.Get(_customerRepository)
                .Setup(r => r.GetByIdAsync(customerId))
                .ReturnsAsync(existingCustomer);

            // Act: Invoke the PutAsync action method with the customer view model
            var result = await _customerController.Put(customerId, customerViewModel);

            // Assert: Verify the expected outcome and make assertions
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okObjectResult = (OkObjectResult)result;
            Assert.AreEqual(existingCustomer, okObjectResult.Value); // Ensure the returned value is the updated customer
            Assert.AreEqual(customerViewModel.EmailAddress, existingCustomer.EmailAddress); // Ensure the customer is updated with the new email address
        }

        [TestMethod]
        public async Task DeleteAsyncTest()
        {
            // Arrange: Set up any required dependencies or test data
            int customerId = 1;
            var existingCustomer = new Customer
            {
                Id = customerId,
                EmailAddress = "test@example.com"
            };

            Mock.Get(_customerRepository)
                .Setup(r => r.GetByIdAsync(customerId))
                .ReturnsAsync(existingCustomer);

            // Act: Invoke the DeleteAsync action method
            var result = await _customerController.Delete(customerId);

            // Assert: Verify the expected outcome and make assertions
            Assert.IsInstanceOfType(result, typeof(NoContentResult));

            // Verify that the customer repository's Delete method was called with the correct customer
            Mock.Get(_customerRepository)
                .Verify(r => r.Delete(existingCustomer), Times.Once);

            // Verify that the customer repository's SaveChangesAsync method was called
            Mock.Get(_customerRepository)
                .Verify(r => r.SaveChangesAsync(), Times.Once);
        }

    }
}
