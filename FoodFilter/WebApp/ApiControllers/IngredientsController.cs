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
/// Ingredients Controller
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class IngredientsController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly IngredientMapper _mapper;
    
    /// <summary>
    /// Ingredients Constructor
    /// </summary>
    /// <param name="bll">Application Business Logic Layer Interface</param>
    /// <param name="autoMapper">Auto Mapper</param>
    public IngredientsController(IAppBLL bll, IMapper autoMapper)
    {
        _bll = bll;
        _mapper = new IngredientMapper(autoMapper);
    }
    
    
    /// <summary>
    /// Get list of Ingredients
    /// </summary>
    /// <returns>List of all Ingredients</returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Ingredient>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Ingredient>>> GetIngredients()
    {
        var vm = await _bll.IngredientService.AllAsync();
        var res = vm.Select(e => _mapper.Map(e))
            .ToList();
        return Ok(res);
    }
    
    
    /// <summary>
    /// Get Ingredient by ID
    /// </summary>
    /// <param name="id">Ingredient ID</param>
    /// <returns>Ingredient object</returns>
    [HttpGet("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Ingredient>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<App.Public.DTO.v1.Ingredient>> GetIngredient(Guid id)
    {
        var ingredient = await _bll.IngredientService.FindAsync(id);

        if (ingredient == null)
        {
            return NotFound();
        }

        var res = _mapper.Map(ingredient);
        return Ok(res);
    }
}