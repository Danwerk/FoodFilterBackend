using System.Net.Mime;
using App.Contracts.BLL;
using App.Public.DTO.Mappers;
using App.Public.DTO.v1;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    /// Get ingredients ordered descending based on time created
    /// </summary>
    /// <param name="limit">Number of records returned</param>
    /// <param name="search">
    /// Phrase which ingredient name or description must contain.
    /// If left blank, all ingredients will be returned.
    /// </param>
    /// <returns>Collection of features</returns>
    /// <response code="200">Collection of ingredients was successfully retrieved.</response>
    /// <response code="401">Not authorized to see the data.</response>
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Ingredient>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces(MediaTypeNames.Application.Json)]
    [HttpGet("{limit}/{search?}")]
    public async Task<ActionResult<IEnumerable<Ingredient>>> GetAllIngredients(int limit, string? search)
    {
        var vm = _bll.IngredientService.GetAll(limit, search);
        var res = vm.Select(e => _mapper.Map(e))
            .ToList();

        return Ok(res);
    }
    
    /// <summary>
    /// Get unconfirmed ingredients ordered ascending based on time created
    /// </summary>
    /// <returns>Collection of unconfirmed ingredients</returns>
    /// <response code="200">Collection of ingredients was successfully retrieved.</response>
    /// <response code="401">Not authorized to see the data.</response>
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Ingredient>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces(MediaTypeNames.Application.Json)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ingredient>>> GetUnconfirmedIngredients()
    {
        var vm = await _bll.IngredientService.GetUnconfirmedIngredients();
        var res = vm.Select(e => _mapper.Map(e))
            .ToList();

        return Ok(res);
    }
    
    
    /// <summary>
    /// Get confirmed ingredients ordered ascending based on time created
    /// </summary>
    /// <returns>Collection of confirmed ingredients</returns>
    /// <response code="200">Collection of ingredients was successfully retrieved.</response>
    /// <response code="401">Not authorized to see the data.</response>
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Ingredient>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces(MediaTypeNames.Application.Json)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ingredient>>> GetConfirmedIngredients()
    {
        var vm = await _bll.IngredientService.GetConfirmedIngredients();
        var res = vm.Select(e => _mapper.Map(e))
            .ToList();

        return Ok(res);
    }


    /// <summary>
    /// Get list of Ingredient names.
    /// </summary>
    /// <returns>List of Ingredient names</returns>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<List<string>>> GetIngredientNames([FromBody] List<Guid> ingredientIds)
    {
        var res = await _bll.IngredientService.GetIngredientNamesAsync(ingredientIds);
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


    /// <summary>
    /// Create new Ingredient
    /// </summary>
    /// <param name="ingredient">New Ingredient object</param>
    /// <returns>Created Ingredient object</returns>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Ingredient>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<App.Public.DTO.v1.Ingredient>> CreateIngredient(Ingredient ingredient)
    {
        ingredient.Id = Guid.NewGuid();
        var bllIngredient = _mapper.Map(ingredient);
        _bll.IngredientService.Add(bllIngredient!);
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetIngredient", new { id = ingredient.Id }, ingredient);
    }
    
    
    /// <summary>
    /// Delete Food with specified id
    /// </summary>
    /// <param name="id">Food ID</param>
    /// <returns>Action result</returns>
    [HttpDelete("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteIngredient(Guid id)
    {
        var ingredient = await _bll.IngredientService.FirstOrDefaultAsync(id);

        if (ingredient == null)
        {
            return NotFound();
        }
        await _bll.IngredientService.RemoveAsync(ingredient.Id);
        await _bll.SaveChangesAsync();

        return Ok();
    }
    
    
    /// <summary>
    /// Update Ingredient with specified id
    /// </summary>
    /// <param name="id">Ingredient ID</param>
    /// <param name="ingredient">Edited Ingredient object that need to be updated</param>
    /// <returns>Action result</returns>
    [HttpPut("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ActionResult<App.Public.DTO.v1.Ingredient>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Ingredient>> EditIngredient(Guid id, Ingredient ingredient)
    {
        if (id != ingredient.Id)
        {
            return BadRequest();
        }

        try
        {
            var ingredientBll = _mapper.Map(ingredient);
            var editedIngredient = _bll.IngredientService.Update(ingredientBll!);
            await _bll.SaveChangesAsync();
            return Ok(editedIngredient);
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }
    }
}