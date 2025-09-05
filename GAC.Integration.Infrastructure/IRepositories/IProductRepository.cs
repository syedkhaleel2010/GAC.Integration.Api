using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities;
using GAC.Integration.Domain.Entities.GAC.Integration.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAC.Integration.Infrastructure.IRepositories
{
    public interface IProductRepository
    {
        Task<bool> CreateProduct(Product entity);
        Task<bool> UpdateProduct(Product entity);
        Task<bool> DeleteProduct(string id);
        Task<ProductDto> GetProductById(string id);
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<bool> ProductExists(string id);
    }
}