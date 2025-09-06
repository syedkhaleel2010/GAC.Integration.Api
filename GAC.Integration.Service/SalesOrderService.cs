using AutoMapper;
using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities;
using GAC.Integration.Infrastructure.Data;
using GAC.Integration.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GAC.Integration.Service
{
    public class SalesOrderService : BaseService, ISalesOrderService
    {
        private readonly ServiceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<SalesOrderService> _logger;
        private readonly IUserSession _userSession;

        public SalesOrderService(ServiceDbContext dbContext, 
            IMapper mapper, 
            IUserSession userSession, 
            ILogger<SalesOrderService> logger) :base(userSession)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userSession = userSession;
            _logger = logger;
        }

        public async Task<SalesOrderDto> CreateSalesOrderAsync(SalesOrderDto salesOrderDto)
        {
            var salesOrder = _mapper.Map<SalesOrder>(salesOrderDto);
            salesOrder.ID = Guid.NewGuid();
            SetCreatedBy(salesOrder);
            // Ensure SalesOrderItems are linked correctly
            foreach (var item in salesOrder.SalesOrderItems)
            {
                item.SalesOrderID = salesOrder.ID;
                item.CreatedBy = salesOrder.CreatedBy;
                item.CreatedAt = salesOrder.CreatedAt;
            }

            await _dbContext.SalesOrders.AddAsync(salesOrder);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<SalesOrderDto>(salesOrder);
        }

        public async Task<SalesOrderDto> GetByIdAsync(Guid id)
        {
            var salesOrder = await _dbContext.SalesOrders
                .Include(so => so.SalesOrderItems)
                .FirstOrDefaultAsync(so => so.ID == id);

            return _mapper.Map<SalesOrderDto>(salesOrder);
        }

        public async Task<IEnumerable<SalesOrderDto>> GetAllAsync()
        {
            var salesOrders = await _dbContext.SalesOrders
                .Include(so => so.SalesOrderItems)
                .ToListAsync();

            return _mapper.Map<IEnumerable<SalesOrderDto>>(salesOrders);
        }

        public async Task<bool> UpdateAsync(SalesOrderDto salesOrderDto)
        {
            var salesOrder = _mapper.Map<SalesOrder>(salesOrderDto);

            _dbContext.SalesOrders.Update(salesOrder);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var salesOrder = await _dbContext.SalesOrders.FindAsync(id);
            if (salesOrder == null)
                return false;

            _dbContext.SalesOrders.Remove(salesOrder);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
