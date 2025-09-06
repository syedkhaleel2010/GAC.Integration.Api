using AutoMapper;
using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities;


namespace GAC.Integration.Domain
{
    public class DtoEntityMapper :Profile
    {
        public DtoEntityMapper()
        {
            CreateMap<CustomerDto, Customer>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<PurchaseOrderDto, PurchaseOrder>()
                .ForMember(dest => dest.PurchaseOrderItems, opt => opt.MapFrom(src => src.PurchaseOrderLineDto))
                .ReverseMap()
                .ForMember(dest => dest.PurchaseOrderLineDto, opt => opt.MapFrom(src => src.PurchaseOrderItems));

            CreateMap<PurchaseOrderItemsDto, PurchaseOrderItems>().ReverseMap();
            CreateMap<SalesOrderDto, SalesOrder>()
                .ForMember(dest => dest.SalesOrderItems, opt => opt.MapFrom(src => src.SalesOrderItems))
                .ReverseMap()
                .ForMember(dest => dest.SalesOrderItems, opt => opt.MapFrom(src => src.SalesOrderItems));
            CreateMap<SalesOrderItemsDto, SalesOrderItems>().ReverseMap();
        }
    }
}
