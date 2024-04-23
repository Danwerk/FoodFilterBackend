using System.Net.Mime;
using App.Common;
using App.Contracts.BLL;
using App.Common.IngredientNutrientDtos;
using App.Public.DTO.Mappers;
using App.Public.DTO.v1;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

/// <summary>
/// IngredientNutrients Controller
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class IngredientNutrientsController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly IngredientNutrientMapper _mapper;

    /// <summary>
    /// IngredientNutrients Constructor
    /// </summary>
    /// <param name="bll">Application Business Logic Layer Interface</param>
    /// <param name="autoMapper">Auto Mapper</param>
    public IngredientNutrientsController(IAppBLL bll, IMapper autoMapper)
    {
        _bll = bll;
        _mapper = new IngredientNutrientMapper(autoMapper);
    }

    /// <summary>
    /// Get ingredientNutrients
    /// </summary>
    /// <param name="limit">Number of records returned</param>
    /// <param name="search">
    /// Phrase which ingredient or nutrient name must contain.
    /// If left blank, all ingredient nutrients will be returned.
    /// </param>
    /// <returns>Collection of ingredient nutrients</returns>
    /// <response code="200">Collection of ingredient nutrients was successfully retrieved.</response>
    /// <response code="401">Not authorized to see the data.</response>
    [ProducesResponseType(typeof(IEnumerable<IngredientNutrient>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces(MediaTypeNames.Application.Json)]
    [HttpGet("{limit}/{search?}")]
    public Task<ActionResult<IEnumerable<IngredientNutrient>>> GetIngredientNutrients(int limit, string? search)
    {
        var vm = _bll.IngredientNutrientService.GetAll(limit, search);

        var groupedByIngredient = vm.GroupBy(n => n.IngredientId);

        var responseList = new List<IngredientNutrient>();
        foreach (var group in groupedByIngredient)
        {
            var ingredient = group.First().Ingredient;
            var ingredientNutrient = new IngredientNutrient
            {
                Ingredient = new Ingredient()
                {
                    Name = ingredient!.Name,
                    KCaloriesPer100Grams = ingredient.KCaloriesPer100Grams,
                    Id = ingredient.Id
                },
                Nutrients = group.ToDictionary(n => n.Nutrient!.Name, n => n.Amount)
            };
            responseList.Add(ingredientNutrient);
        }

        return Task.FromResult<ActionResult<IEnumerable<IngredientNutrient>>>(Ok(responseList));
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateIngredientNutrients(IngredientNutrientUpdateDto dto)
    {
        if (User.IsInRole(RoleNames.Admin))
        {
            try
            {
                await _bll.IngredientNutrientService.UpdateIngredientNutrientsAsync(dto.IngredientId, dto.Nutrients);
                return Ok();
            }
            catch (Exception)
            {
                // Log the exception or handle it appropriately
                return BadRequest("Failed to update ingredient nutrients.");
            }
        }
        return NotFound();
    }
}