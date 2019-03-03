using AutoMapper;
using Models;
using Web.Dto;

namespace Web.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {           
            //In real world scenario, mapping must be a lot more complicated but for this test project there is almost nothing to do.            
           CreateMap<ProductData, ProductResponseDto>();
        }        
    }
}