using AutoMapper;
using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities;
using GAC.Integration.Infrastructure.IRepositories;
using GAC.Integration.Service.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAC.Integration.Service
{
    public class ProductService :  BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;
        private readonly IUserSession _userSession;
        public ProductService(IProductRepository productRepository, IMapper mapper,ILogger<ProductService> logger, IUserSession userSession) : base(userSession)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
            _userSession = userSession;
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            _logger.LogInformation("Creating a new customer.");
            productDto.ID = Guid.NewGuid();
            var entity = _mapper.Map<Product>(productDto);
            SetCreatedBy(entity);
            await _productRepository.CreateProduct(entity);
            return await Task.FromResult(productDto);
        }

        public async Task<ProductDto> UpdateProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.UpdateProduct(product);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DeleteProductAsync(Guid Id)
        {
            return await _productRepository.DeleteProduct(Id);
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid Id)
        {
            return await _productRepository.GetProductById(Id);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            return await _productRepository.GetProducts();
        }

        public async Task<bool> ProductExistsAsync(Guid Id)
        {
            return await _productRepository.ProductExists(Id);
        }
    }
}
