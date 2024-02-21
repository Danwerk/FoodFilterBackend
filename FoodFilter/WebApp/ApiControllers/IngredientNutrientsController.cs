using System.Net.Mime;
using App.Contracts.BLL;
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
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.IngredientNutrient>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces(MediaTypeNames.Application.Json)]
    [HttpGet("{limit}/{search?}")]
    public async Task<ActionResult<IEnumerable<IngredientNutrient>>> GetIngredientNutrients(int limit, string? search)
    {
        var vm = _bll.IngredientNutrientService.GetAll(limit, search);
        var res = vm.Select(e => _mapper.Map(e))
            .ToList();

        return Ok(res);
    }
}