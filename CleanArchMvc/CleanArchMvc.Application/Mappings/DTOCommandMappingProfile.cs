using AutoMapper;
using CleanArchMvc.Application.CQRS.Products.Commands;
using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Mappings
{
    public class DTOCommandMappingProfile : Profile
    {
        public DTOCommandMappingProfile()
        {
            CreateMap<ProductDTO, ProductCreateCommand>();
            CreateMap<ProductDTO, ProductUpdateCommand>();
        }
    }
}
