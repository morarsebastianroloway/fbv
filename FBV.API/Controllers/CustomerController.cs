﻿using AutoMapper;
using FBV.API.ViewModels;
using FBV.DAL.Contracts;
using FBV.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FBV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(IMapper mapper, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<IEnumerable<CustomerViewModel>> GetAsync()
        {
            var result = await _customerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerViewModel>>(result);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<CustomerViewModel> Get(int id)
        {
            var result = await _customerRepository.GetByIdAsync(id);
            return _mapper.Map<CustomerViewModel>(result);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerViewModel value)
        {
            try
            {
                // Let's set the id as 0 so we generate a new id from db
                value.Id = 0;

                var result = await _customerRepository.CreateAsync(_mapper.Map<Customer>(value));
                return CreatedAtAction(nameof(Get), new { id = result.Id }, _mapper.Map<CustomerViewModel>(result));
            }
            catch (Exception ex)
            {
                // Handle any exception that occurred during the create process
                return StatusCode(500, $"An error occurred while creating the entity: {ex.Message}");
            }
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CustomerViewModel value)
        {
            try
            {
                if (id != value.Id)
                {
                    return BadRequest("The ID in the URL does not match the ID in the entity.");
                }

                var existingCustomer = await _customerRepository.GetByIdAsync(id);
                if (existingCustomer == null)
                {
                    return NotFound();
                }

                // Update the properties of the existing customer entity with the values from the view model
                existingCustomer.EmailAddress = value.EmailAddress;

                await _customerRepository.UpdateAsync(existingCustomer);

                return Ok(existingCustomer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the entity: {ex.Message}");
            }
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var entityToDelete = await _customerRepository.GetByIdAsync(id);
                if (entityToDelete == null)
                {
                    return NotFound();
                }

                await _customerRepository.DeleteAsync(entityToDelete);

                return NoContent();
            }
            catch (Exception ex)
            {
                // Handle any exception that occurred during the delete process
                return StatusCode(500, $"An error occurred while deleting the entity: {ex.Message}");
            }
        }
    }
}
