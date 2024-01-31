using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class CategoryMapper: BaseMapper<App.BLL.DTO.Category, App.Public.DTO.v1.Category>
{
    public CategoryMapper(IMapper mapper) : base(mapper)
    {
    }
}