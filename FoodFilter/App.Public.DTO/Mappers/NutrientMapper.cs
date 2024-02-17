using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class NutrientMapper: BaseMapper<App.BLL.DTO.Nutrient, App.Public.DTO.v1.Nutrient>
{
    public NutrientMapper(IMapper mapper) : base(mapper)
    {
    }
    
}