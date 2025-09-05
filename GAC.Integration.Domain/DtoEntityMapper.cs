using AutoMapper;
using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities;
using GAC.Integration.Domain.Entities.GAC.Integration.Domain.Entities;

namespace GAC.Integration.Domain
{
    public class DtoEntityMapper :Profile
    {
        public DtoEntityMapper()
        {
            CreateMap<CustomerDto, Customer>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
        }
    }
}
