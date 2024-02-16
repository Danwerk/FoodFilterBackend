using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class AllergenMapper: BaseMapper<BLL.DTO.Allergen, App.Domain.Allergen>
{
    public AllergenMapper(IMapper mapper) : base(mapper)
    {
    }
}