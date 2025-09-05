using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAC.Integration.Service.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> CreateProductAsync(ProductDto productDto);
        Task<ProductDto> UpdateProductAsync(ProductDto productDto);
        Task<bool> DeleteProductAsync(string productCode);
        Task<ProductDto> GetProductByIdAsync(string productCode);
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<bool> ProductExistsAsync(string productCode);
    }
}
