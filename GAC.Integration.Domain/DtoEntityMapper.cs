using AutoMapper;
using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities;
using GAC.Integration.Domain.Entities.GAC.Integration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAC.Integration.Domain
{
    public class DtoEntityMapper :Profile
    {
        public DtoEntityMapper()
        {
            CreateMap<CustomerDto, Customer>().ReverseMap();
        }

    }
}
