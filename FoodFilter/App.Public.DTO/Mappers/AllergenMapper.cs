using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class AllergenMapper: BaseMapper<App.BLL.DTO.Allergen, App.Public.DTO.v1.Allergen>
{
    public AllergenMapper(IMapper mapper) : base(mapper)
    {
    }
    
}