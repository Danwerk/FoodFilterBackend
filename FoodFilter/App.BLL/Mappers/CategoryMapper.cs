using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class CategoryMapper: BaseMapper<BLL.DTO.Category, App.Domain.Category>
{
    public CategoryMapper(IMapper mapper) : base(mapper)
    {
    }
}