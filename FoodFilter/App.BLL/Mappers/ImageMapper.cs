using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class ImageMapper: BaseMapper<BLL.DTO.Image, App.Domain.Image>
{
    public ImageMapper(IMapper mapper) : base(mapper)
    {
    }
}