using FluentValidation;
using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities;
using GAC.Integration.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GAC.Integration.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ServiceControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;

        public ProductsController(ILogger<ProductsController> logger, IProductService productService)
            : base(logger)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto product)
        {
            try
            {
                var result = await _productService.CreateProductAsync(product);
                return OkServiceResponse(result);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Error occurred while creating product.");
                return HandleValidationException(ex);
            }
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto product)
        {
            try
            {
                var result = await _productService.UpdateProductAsync(product);
                return OkServiceResponse(result);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Error occurred while updating product.");
                return HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while updating product.");
                return HandleOtherException(ex);
            }
        }

        [HttpDelete("DeleteProduct/{Id}")]
        public async Task<IActionResult> DeleteProduct(Guid Id)
        {
            try
            {
                var result = await _productService.DeleteProductAsync(Id);
                if (!result)
                    return NotFound($"Product with code '{Id}' not found.");
                return OkServiceResponse(result, "Product deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting product.");
                return HandleOtherException(ex);
            }
        }

        [HttpGet("GetProductById/{Id}")]
        public async Task<IActionResult> GetProductById(Guid Id)
        {
            try
            {
                var result = await _productService.GetProductByIdAsync(Id);
                if (result == null)
                    return NotFound($"Product with code '{Id}' not found.");
                return OkServiceResponse(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving product.");
                return HandleOtherException(ex);
            }
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var result = await _productService.GetProductsAsync();
                return OkServiceResponse(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving products.");
                return HandleOtherException(ex);
            }
        }

        [HttpPost("bulk-insert")]
        public async Task<IActionResult> BulkInsertProducts([FromBody] List<ProductDto> productsDto)
        {
            if (productsDto == null || !productsDto.Any())
                return BadRequest("No products provided for bulk insert.");

            return OkServiceResponse(await _productService.BulkInsertProducts(productsDto));
           
        }

    }
}
