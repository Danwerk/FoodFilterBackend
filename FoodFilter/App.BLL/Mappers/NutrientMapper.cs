using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class NutrientMapper: BaseMapper<BLL.DTO.Nutrient, App.Domain.Nutrient>
{
    public NutrientMapper(IMapper mapper) : base(mapper)
    {
    }
}