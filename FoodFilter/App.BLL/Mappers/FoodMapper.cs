using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class FoodMapper: BaseMapper<BLL.DTO.Food, App.Domain.Food>
{
    public FoodMapper(IMapper mapper) : base(mapper)
    {
    }
}