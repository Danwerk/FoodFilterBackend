using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class IngredientMapper: BaseMapper<App.BLL.DTO.Ingredient, App.Public.DTO.v1.Ingredient>
{
    public IngredientMapper(IMapper mapper) : base(mapper)
    {
    }
}