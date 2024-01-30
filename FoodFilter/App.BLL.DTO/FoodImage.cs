using Domain.Base;

namespace App.BLL.DTO;

public class FoodImage : DomainEntityId
{
    public Guid FoodId { get; set; }
    public Guid ImageId { get; set; }
}