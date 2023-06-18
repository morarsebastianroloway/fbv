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
        private readonly IPurchaseOrderProcessor _purchaseOrderProcessor;

        public PurchaseOrderController(
            IMapper mapper,
            IPurchaseOrderRepository purchaseOrderRepository,
            IPurchaseOrderProcessor purchaseOrderProcessor)
        {
            _mapper = mapper;
            _purchaseOrderRepository = purchaseOrderRepository;
            _purchaseOrderProcessor = purchaseOrderProcessor;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Let's set the id as 0 so we generate a new id from db
                value.Id = 0;

                // We create the po object that we will save to db
                var po = _mapper.Map<PurchaseOrder>(value);

                var result = await _purchaseOrderProcessor.ProcessNewOrderAsync(po);

                return CreatedAtAction(nameof(Get), new { id = result.Id }, _mapper.Map<PurchaseOrderViewModel>(result));
            }
            catch (Exception ex)
            {
                // Handle any exception that occurred during the create process
                return StatusCode(500, $"An error occurred while creating the entity: {ex.Message}");
            }
        }
    }
}
