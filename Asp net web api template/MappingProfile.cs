using AutoMapper;
using Entities.Models;
using Shared.DTOs;

namespace Asp_net_web_api_template
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ForCtorParam("Age", opt => opt.MapFrom(x => DateTime.Today.Year - x.DateBirth.Year));
        }
    }
}
