using System.Diagnostics;
using System.Globalization;
using System.Net.Mime;
using App.Common;
using App.Common.CsvDtos;
using App.Contracts.DAL;
using App.Domain;
using Asp.Versioning;
using CsvHelper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

/// <summary>
/// Foods Controller
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class FilesController : ControllerBase
{
    private readonly IAppUOW _uow;
    private readonly ILogger<FilesController> _logger;

    /// <summary>
    /// Files Constructor
    /// </summary>
    /// <param name="uow">Application Unit Of Work Interface</param>
    /// <param name="logger">Logger interface</param>
    public FilesController(IAppUOW uow, ILogger<FilesController> logger)
    {
        _uow = uow;
        _logger = logger;
    }


    /// <summary>
    /// Upload CSV File
    /// </summary>
    /// <returns>Food object</returns>
    /// <response code="200">CSV file was successfully saved.</response>
    /// <response code="400">Uploading did not success.</response>
    /// <response code="401">Unauthorized - unable to get the data.</response>
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType( StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("File is empty");
        }

        try
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<CsvRecordDto>();

                var unit = await _uow.UnitRepository.FirstOrDefaultAsync(UnitTypes.G);
                Guid unitId;
                if (unit == null)
                {
                    var unitToSave = new Unit
                    {
                        UnitName = UnitTypes.G
                    };
                    _uow.UnitRepository.Add(unitToSave);
                    await _uow.SaveChangesAsync();
                    unitId = unitToSave.Id;
                }
                else
                {
                    unitId = unit!.Id;
                }

                var ingredients = new List<Ingredient>();
                var ingredientNutrients = new List<IngredientNutrient>();
                Stopwatch stopwatch = Stopwatch.StartNew();

                foreach (var record in records)
                {
                    Guid id;
                    var existingIngredient = await _uow.IngredientRepository.FirstOrDefaultAsync(record.IngredientName);
                    // if ingredient exist, then update it and delete all ingredientNutrients associated with it
                    if (existingIngredient != null)
                    {
                        // Update the energy value of the existing ingredient
                        existingIngredient.KCaloriesPer100Grams = record.Energy;
                        _uow.IngredientRepository.Update(existingIngredient);
                        id = existingIngredient.Id;

                        await _uow.IngredientNutrientRepository.DeleteRangeAsync(i =>
                            i.IngredientId == existingIngredient.Id);
                    }
                    else
                    {
                        id = Guid.NewGuid();
                        var ingredient = new Ingredient
                        {
                            Id = id,
                            Name = record.IngredientName,
                            IsConfirmed = true,
                            KCaloriesPer100Grams = record.Energy,
                        };
                        ingredients.Add(ingredient);
                    }

                    await MapAndSaveNutrient(NutrientNames.FAT, decimal.Parse(record.Fat),  id,
                        unitId,
                        ingredientNutrients);
                    await MapAndSaveNutrient(NutrientNames.SATURATED_FATTY_ACIDS,
                        decimal.Parse(record.SaturatedFattyAcids),  id, unitId, ingredientNutrients);
                    await MapAndSaveNutrient(NutrientNames.CARBOHYDRATES, decimal.Parse(record.Carbohydrates),
                         id, unitId, ingredientNutrients);
                    await MapAndSaveNutrient(NutrientNames.SUGAR, decimal.Parse(record.Sugar),  id, unitId,
                        ingredientNutrients);
                    await MapAndSaveNutrient(NutrientNames.FIBER, decimal.Parse(record.Fiber),  id, unitId,
                        ingredientNutrients);
                    await MapAndSaveNutrient(NutrientNames.PROTEIN, decimal.Parse(record.Protein),  id,
                        unitId, ingredientNutrients);
                    await MapAndSaveNutrient(NutrientNames.SALT, decimal.Parse(record.Salt),  id, unitId,
                        ingredientNutrients);
                }

                await _uow.IngredientRepository.AddRangeAsync(ingredients);
                await _uow.IngredientNutrientRepository.AddRangeAsync(ingredientNutrients);

                await _uow.SaveChangesAsync();
                stopwatch.Stop(); 

                TimeSpan elapsedTime = stopwatch.Elapsed; 

                _logger.LogInformation($"Time taken for saving: {elapsedTime.TotalMilliseconds} milliseconds");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        var f = file;
        return Ok(file);
    }


    private async Task MapAndSaveNutrient(string nutrientName, decimal value, Guid ingredientId, Guid unitId,
        List<IngredientNutrient> ingredientNutrients)
    {
        var nutrient = await _uow.NutrientRepository.FirstOrDefaultAsync(nutrientName.ToLower());

        if (nutrient != null)
        {
            var ingredientNutrient = new IngredientNutrient
            {
                IngredientId = ingredientId,
                NutrientId = nutrient.Id,
                Amount = value,
                UnitId = unitId
            };
            ingredientNutrients.Add(ingredientNutrient);
        }
    }
}