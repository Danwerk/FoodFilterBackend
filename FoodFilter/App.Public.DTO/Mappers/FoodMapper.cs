using AutoMapper;
using Base.DAL;
using FoodNutritionCalculation = App.Public.DTO.v1.FoodNutritionCalculation;

namespace App.Public.DTO.Mappers;

public class FoodMapper: BaseMapper<App.BLL.DTO.Food, App.Public.DTO.v1.Food>
{
    public FoodMapper(IMapper mapper) : base(mapper)
    {
    }
    
}