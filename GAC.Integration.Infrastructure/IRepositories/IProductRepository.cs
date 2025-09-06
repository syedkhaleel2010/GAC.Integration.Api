using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities;

namespace GAC.Integration.Infrastructure.IRepositories
{
    public interface IProductRepository
    {
        Task<bool> CreateProduct(Product entity);
        Task<bool> UpdateProduct(Product entity);
        Task<bool> DeleteProduct(Guid id);
        Task<ProductDto> GetProductById(Guid id);
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<bool> ProductExists(Guid id);
    }
}