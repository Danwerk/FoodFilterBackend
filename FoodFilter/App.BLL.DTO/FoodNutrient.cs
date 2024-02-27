using System.Text.Json.Serialization;
using Domain.Base;

namespace App.BLL.DTO;

public class FoodNutrient : DomainEntityId
{
    public Guid NutrientId { get; set; }
    [JsonIgnore]
    public Nutrient? Nutrient { get; set; }
    public Guid FoodId { get; set; }
    public Guid UnitId { get; set; }
    //public string NutrientName { get; set; } = default!;
    public decimal AmountPer100Grams { get; set; }
    public decimal AmountPerFoodTotalWeight { get; set; }
   // public string UnitName { get; set; } = default!;



}