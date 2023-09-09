using AutoMapper;
using BeerWholesaleManagementSystem.Core.DTO;
using BeerWholesaleManagementSystem.Core.Models;

namespace BeerWholesaleManagementSystem.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Domain(database) to DTO
        CreateMap<Beer, BeerDto>();
        CreateMap<SaleBeer, SaleBeerDto>();
        CreateMap<Stock, StockDto>();

        // DTO to Domain or Database
        CreateMap<BeerDto, Beer>();
        CreateMap<SaleBeerDto, SaleBeer>();
        CreateMap<StockDto, Stock>();
    }
}
