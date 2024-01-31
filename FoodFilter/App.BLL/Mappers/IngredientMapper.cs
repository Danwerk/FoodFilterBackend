using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class IngredientMapper: BaseMapper<BLL.DTO.Ingredient, App.Domain.Ingredient>
{
    public IngredientMapper(IMapper mapper) : base(mapper)
    {
    }
}