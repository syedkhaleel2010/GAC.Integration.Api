using AutoMapper;
using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities;
using GAC.Integration.Infrastructure.Data;
using GAC.Integration.Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace MG.Marine.Ticketing.SQL.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly ServiceDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductRepository(ServiceDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            await _dbContext.Products.AddAsync(product);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var entity = await _dbContext.Products.FindAsync(product.ProductCode);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Product not found");

            entity.Title = product.Title ?? string.Empty;
            entity.Description = product.Description;
            entity.Length = product.Length;
            entity.Width = product.Width;
            entity.Height = product.Height;
            entity.Weight = product.Weight;
            entity.UpdatedBy = product.UpdatedBy;
            entity.UpdatedAt = product.UpdatedAt;

            _dbContext.Products.Update(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            var entity = await _dbContext.Products.FindAsync(id);
            if (entity == null)
                return false;

            _dbContext.Products.Remove(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<ProductDto> GetProductById(Guid id)
        {
            var entity = await _dbContext.Products.FindAsync(id);
            return entity != null ? _mapper.Map<ProductDto>(entity) : null;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var entities = await _dbContext.Products.ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(entities);
        }

        public async Task<bool> ProductExists(Guid id)
        {
            var exists = await _dbContext.Products.AnyAsync(x => x.ID == id);
            return exists;
        }
    }
}
