using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class IngredientNutrientMapper: BaseMapper<App.BLL.DTO.IngredientNutrient, App.Public.DTO.v1.IngredientNutrient>
{
    public IngredientNutrientMapper(IMapper mapper) : base(mapper)
    {
    }
}