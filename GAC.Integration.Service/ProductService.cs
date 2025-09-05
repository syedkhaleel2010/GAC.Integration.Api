using AutoMapper;
using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities;
using GAC.Integration.Infrastructure.IRepositories;
using GAC.Integration.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAC.Integration.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.CreateProduct(product);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> UpdateProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.UpdateProduct(product);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DeleteProductAsync(string productCode)
        {
            return await _productRepository.DeleteProduct(productCode);
        }

        public async Task<ProductDto> GetProductByIdAsync(string productCode)
        {
            return await _productRepository.GetProductById(productCode);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            return await _productRepository.GetProducts();
        }

        public async Task<bool> ProductExistsAsync(string productCode)
        {
            return await _productRepository.ProductExists(productCode);
        }
    }
}
