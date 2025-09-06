using AutoMapper;
using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities;
using GAC.Integration.Infrastructure.IRepositories;
using GAC.Integration.Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace GAC.Integration.Service
{
    public class PurchaseOrderService :BaseService, IPurchaseOrderService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PurchaseOrderService> _logger;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IUserSession _userSession;
        public PurchaseOrderService(IMapper mapper,ILogger<PurchaseOrderService> logger,
            IPurchaseOrderRepository purchaseOrderRepository, IUserSession userSession):base(userSession)
        {
            _mapper = mapper;
            _logger = logger;
            _purchaseOrderRepository = purchaseOrderRepository;
            _userSession = userSession;
        }
        public async Task<PurchaseOrderDto> CreatePurchaseOrder(PurchaseOrderDto purchaseOrder)
        {
            purchaseOrder.ID = Guid.NewGuid();
            foreach (var item in purchaseOrder.PurchaseOrderLineDto)
            {
                item.ID = Guid.NewGuid();
                item.PurchaseOrderID = purchaseOrder.ID;
                
            }
            var entity = _mapper.Map<PurchaseOrder>(purchaseOrder);
            SetCreatedBy(entity);
            await _purchaseOrderRepository.CreatePurchaseOrder(entity);
            return purchaseOrder;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PurchaseOrderDto>> GetAllAsync()
        {
            return await _purchaseOrderRepository.GetAllAsync();
        }

        public async Task<PurchaseOrderDto> GetByIdAsync(Guid id)
        {
            return await _purchaseOrderRepository.GetByIdAsync(id);
        }

        public Task<bool> UpdateAsync(PurchaseOrderDto purchaseOrder)
        {
             var entity = _mapper.Map<PurchaseOrder>(purchaseOrder);
            return _purchaseOrderRepository.UpdateAsync(entity);

        }
    }
}
