using AutoMapper;
using FBV.API.ViewModels;
using FBV.DAL.Contracts;
using FBV.DAL.Repositories;
using FBV.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FBV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseOrderController(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // GET: api/<PurchaseOrderController>
        [HttpGet]
        public async Task<IEnumerable<PurchaseOrderViewModel>> GetAsync()
        {
            var result = await _unitOfWork.PurchaseOrderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PurchaseOrderViewModel>>(result);
        }

        // GET api/<PurchaseOrderController>/5
        [HttpGet("{id}")]
        public async Task<PurchaseOrderViewModel> Get(int id)
        {
            var result = await _unitOfWork.PurchaseOrderRepository.GetByIdAsync(id);
            return _mapper.Map<PurchaseOrderViewModel>(result);
        }

        // POST api/<PurchaseOrderController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PurchaseOrderViewModel value)
        {
            try
            {
                // Let's set the id as 0 so we generate a new id from db
                value.Id = 0;

                // We create the po object that we will save to db
                var po = _mapper.Map<PurchaseOrder>(value);

                // TODO: Calculate the total price from PO lines
                // TODO: Activate memberships if necessary

                // Add the PO in the database
                var result = await _unitOfWork.PurchaseOrderRepository.CreateAsync(po);

                // Save all the changes we did
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(Get), new { id = result.Id }, _mapper.Map<PurchaseOrderViewModel>(result));
            }
            catch (Exception ex)
            {
                // Handle any exception that occurred during the create process
                return StatusCode(500, $"An error occurred while creating the entity: {ex.Message}");
            }
        }

        // PUT api/<PurchaseOrderController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PurchaseOrderViewModel value)
        {
            try
            {
                if (id != value.Id)
                {
                    return BadRequest("The ID in the URL does not match the ID in the entity.");
                }

                var existingPurchaseOrder = await _unitOfWork.PurchaseOrderRepository.GetByIdAsync(id);
                if (existingPurchaseOrder == null)
                {
                    return NotFound();
                }

                // Update the properties of the existing entity with the values from the view model
                existingPurchaseOrder.CustomerId = value.CustomerId;
                //existingPurchaseOrder.TotalPrice = value.Lines;
                // TODO: Fill with all necessary changes
                // TODO: Remove all PO lines and add the new ones (remove the subscription if it was bought and removed)

                await _unitOfWork.PurchaseOrderRepository.UpdateAsync(existingPurchaseOrder);

                return Ok(existingPurchaseOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the entity: {ex.Message}");
            }
        }

        // DELETE api/<PurchaseOrderController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var entityToDelete = await _unitOfWork.PurchaseOrderRepository.GetByIdAsync(id);
                if (entityToDelete == null)
                {
                    return NotFound();
                }

                await _unitOfWork.PurchaseOrderRepository.DeleteAsync(entityToDelete);

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
