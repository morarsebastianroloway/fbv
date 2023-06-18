using AutoMapper;
using FBV.API.Managers;
using FBV.API.ViewModels;
using FBV.DAL.Contracts;
using FBV.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FBV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IPurchaseOrderProcessor _purchaseOrderManager;

        public PurchaseOrderController(
            IMapper mapper,
            IPurchaseOrderRepository purchaseOrderRepository,
            IPurchaseOrderProcessor purchaseOrderManager)
        {
            _mapper = mapper;
            _purchaseOrderRepository = purchaseOrderRepository;
            _purchaseOrderManager = purchaseOrderManager;
        }

        // GET: api/<PurchaseOrderController>
        [HttpGet]
        public async Task<IEnumerable<PurchaseOrderViewModel>> GetAsync()
        {
            var result = await _purchaseOrderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PurchaseOrderViewModel>>(result);
        }

        // GET api/<PurchaseOrderController>/5
        [HttpGet("{id}")]
        public async Task<PurchaseOrderViewModel> Get(int id)
        {
            var result = await _purchaseOrderRepository.GetByIdAsync(id);
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

                var result = await _purchaseOrderManager.CreatePurchaseOrderAsync(po);

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

                var existingPurchaseOrder = await _purchaseOrderRepository.GetByIdAsync(id);
                if (existingPurchaseOrder == null)
                {
                    return NotFound();
                }

                // Update the properties of the existing entity with the values from the view model
                existingPurchaseOrder.CustomerId = value.CustomerId;
                //existingPurchaseOrder.TotalPrice = value.Lines;
                // TODO: Fill with all necessary changes
                // TODO: Remove all PO lines and add the new ones (remove the subscription if it was bought and removed)

                await _purchaseOrderRepository.UpdateAsync(existingPurchaseOrder);

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
                var entityToDelete = await _purchaseOrderRepository.GetByIdAsync(id);
                if (entityToDelete == null)
                {
                    return NotFound();
                }

                await _purchaseOrderRepository.DeleteAsync(entityToDelete);

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
