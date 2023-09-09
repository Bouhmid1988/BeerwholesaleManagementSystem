using AutoMapper;
using BeerWholesaleManagementSystem.Core.DTO;
using BeerWholesaleManagementSystem.Core.Models;

namespace BeerWholesaleManagementSystem.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Domain(database) to Resource
        CreateMap<Beer, BeerDto>();
        // Resources to Domain or Database
        CreateMap<BeerDto, Beer>();
    }
}
