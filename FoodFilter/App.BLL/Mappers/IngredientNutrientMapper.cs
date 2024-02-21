using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class IngredientNutrientMapper: BaseMapper<BLL.DTO.IngredientNutrient, App.Domain.IngredientNutrient>
{
    public IngredientNutrientMapper(IMapper mapper) : base(mapper)
    {
    }
}