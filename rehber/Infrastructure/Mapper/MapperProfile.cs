using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace rehber.Infrastructure.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ProductDtoForInsertion, Products>();
            CreateMap<ProductDtoForUpdate, Products>().ReverseMap();
            CreateMap<UserWithRolesDto, Users>();
            CreateMap<UserWithRolesDto, Users>().ReverseMap();
            CreateMap<UserDtoForCreation, Users>();
            CreateMap<UserDtoForUpdate, Users>().ReverseMap();
        }
    }
}
