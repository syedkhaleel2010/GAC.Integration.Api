using GAC.Integration.Api.Controllers;
using GAC.Integration.Domain.Dto;
using GAC.Integration.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GAC.Integration.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseOrderController : ServiceControllerBase
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly ILogger<PurchaseOrderController> _logger;

        public PurchaseOrderController(IPurchaseOrderService purchaseOrderService, ILogger<PurchaseOrderController> logger) : base(logger)
        {
            _purchaseOrderService = purchaseOrderService;
            _logger = logger;   
        }

        // Create Purchase Order
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PurchaseOrderDto purchaseOrderDto)
        {
            var result = await _purchaseOrderService.CreatePurchaseOrder(purchaseOrderDto);
            
            return OkServiceResponse(result);
        }

        // Get Purchase Order by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseOrderDto>> GetById(Guid id)
        {
            var result = await _purchaseOrderService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // Get All Purchase Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseOrderDto>>> GetAll()
        {
            var result = await _purchaseOrderService.GetAllAsync();
            return Ok(result);
        }

        // Update Purchase Order
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] PurchaseOrderDto purchaseOrderDto)
        {
            if (id != purchaseOrderDto.ID)
                return BadRequest("ID mismatch");

            var success = await _purchaseOrderService.UpdateAsync(purchaseOrderDto);
            if (!success)
                return NotFound();

            return BadRequest();
        }

        // Delete Purchase Order
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var success = await _purchaseOrderService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
