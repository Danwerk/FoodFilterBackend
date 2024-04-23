using CsvHelper.Configuration.Attributes;

namespace App.Common.CsvDtos;

public class CsvRecordDto
{
    [Name("Name (ENG)")]
    public string IngredientName { get; set; } = default!;
    [Name("Energy")]
    public decimal Energy { get; set; }
    // Nutrients
    [Name(NutrientNames.FAT)]
    public string Fat { get; set; } = default!;
    [Name(NutrientNames.SATURATED_FATTY_ACIDS)]
    public string SaturatedFattyAcids { get; set; } = default!;
    [Name(NutrientNames.CARBOHYDRATES)]
    public string Carbohydrates { get; set; } = default!;
    [Name(NutrientNames.SUGAR)]
    public string Sugar { get; set; } = default!;
    [Name(NutrientNames.FIBER)]
    public string Fiber { get; set; } = default!;
    [Name(NutrientNames.PROTEIN)]
    public string Protein { get; set; } = default!;
    [Name(NutrientNames.SALT)]
    public string Salt { get; set; } = default!;
}