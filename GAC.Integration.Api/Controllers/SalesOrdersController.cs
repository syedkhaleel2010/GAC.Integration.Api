using GAC.Integration.Api.Controllers;
using GAC.Integration.Domain.Dto;
using GAC.Integration.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GAC.Integration.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesOrdersController : ServiceControllerBase
    {
        private readonly ISalesOrderService _salesOrderService;
        private readonly ILogger<SalesOrdersController> _logger;
        
        public SalesOrdersController(ISalesOrderService salesOrderService, ILogger<SalesOrdersController> logger) : base(logger)
        {
            _salesOrderService = salesOrderService;
            _logger = logger;
        }

        /// <summary>
        /// Create a new Sales Order with items
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SalesOrderDto salesOrderDto)
        {
            var result = await _salesOrderService.CreateSalesOrderAsync(salesOrderDto);
            return OkServiceResponse(result);
        }

        /// <summary>
        /// Get a Sales Order by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _salesOrderService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Get all Sales Orders
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesOrderDto>>> GetAll()
        {
            var result = await _salesOrderService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Update an existing Sales Order
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SalesOrderDto salesOrderDto)
        {
            if (id != salesOrderDto.ID)
                return BadRequest("ID mismatch");

            var updated = await _salesOrderService.UpdateAsync(salesOrderDto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Delete a Sales Order by ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _salesOrderService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
