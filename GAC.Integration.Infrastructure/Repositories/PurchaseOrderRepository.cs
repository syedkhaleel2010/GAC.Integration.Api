using AutoMapper;
using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities;
using GAC.Integration.Infrastructure.Data;
using GAC.Integration.Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GAC.Integration.Infrastructure.Repositories
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly ServiceDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PurchaseOrderRepository(ServiceDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            // Ensure PurchaseOrderItems are linked correctly
            foreach (var item in purchaseOrder.PurchaseOrderItems)
            {
                item.PurchaseOrderID = purchaseOrder.ID;
                item.CreatedBy = "System";
                item.CreatedAt = DateTime.UtcNow;
            }

            await _dbContext.PurchaseOrders.AddAsync(purchaseOrder);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<IEnumerable<PurchaseOrderDto>> GetAllAsync()
        {
            var orders = await _dbContext.PurchaseOrders
                .Include(po => po.PurchaseOrderItems)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PurchaseOrderDto>>(orders);
        }

        public async Task<PurchaseOrderDto> GetByIdAsync(Guid id)
        {
            var order = await _dbContext.PurchaseOrders
                .Include(po => po.PurchaseOrderItems)
                .FirstOrDefaultAsync(po => po.ID == id);

            return _mapper.Map<PurchaseOrderDto>(order);
        }

        public async Task<bool> UpdateAsync(PurchaseOrder purchaseOrder)
        {
            _dbContext.PurchaseOrders.Update(purchaseOrder);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
