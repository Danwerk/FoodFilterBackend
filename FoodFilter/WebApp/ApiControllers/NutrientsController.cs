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
/// Nutrients Controller
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class NutrientsController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly NutrientMapper _mapper;
    
    /// <summary>
    /// Nutrients Constructor
    /// </summary>
    /// <param name="bll">Application Business Logic Layer Interface</param>
    /// <param name="autoMapper">Auto Mapper</param>
    public NutrientsController(IAppBLL bll, IMapper autoMapper)
    {
        _bll = bll;
        _mapper = new NutrientMapper(autoMapper);
    }
    
    
    /// <summary>
    /// Get list of Nutrients
    /// </summary>
    /// <returns>List of all Nutrients</returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<Nutrient>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<Nutrient>>> GetNutrients()
    {
        var vm = await _bll.NutrientService.AllAsync();
        var res = vm.Select(e => _mapper.Map(e))
            .ToList();
        return Ok(res);
    }
    
    /// <summary>
    /// Get Nutrient by ID
    /// </summary>
    /// <param name="id">Nutrient ID</param>
    /// <returns>Nutrient object</returns>
    [HttpGet("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Nutrient), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Nutrient>> GetNutrient(Guid id)
    {
        var nutrient = await _bll.NutrientService.FindAsync(id);

        if (nutrient == null)
        {
            return NotFound();
        }

        var res = _mapper.Map(nutrient);
        return Ok(res);
    }
    
    /// <summary>
    /// Create new Nutrient
    /// </summary>
    /// <param name="nutrient">New Nutrient object</param>
    /// <returns>Created Nutrient object</returns>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Nutrient), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Nutrient>> CreateNutrient(Nutrient nutrient)
    {
        var bllNutrient = _mapper.Map(nutrient);
        _bll.NutrientService.Add(bllNutrient!);
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetNutrient", new { id = nutrient.Id }, nutrient);
    }
    
    
    /// <summary>
    /// Delete Nutrient with specified id
    /// </summary>
    /// <param name="id">Nutrient ID</param>
    /// <returns>Action result</returns>
    [HttpDelete("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteNutrient(Guid id)
    {
        var nutrient = await _bll.NutrientService.FirstOrDefaultAsync(id);

        if (nutrient == null)
        {
            return NotFound();
        }
        await _bll.NutrientService.RemoveAsync(nutrient.Id);
        await _bll.SaveChangesAsync();

        return Ok();
    }
    
    /// <summary>
    /// Update Nutrient with specified id
    /// </summary>
    /// <param name="id">Nutrient ID</param>
    /// <param name="nutrient">Edited Nutrient object that need to be updated</param>
    /// <returns>Action result</returns>
    [HttpPut("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ActionResult<Nutrient>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Ingredient>> EditIngredient(Guid id, Nutrient nutrient)
    {
        if (id != nutrient.Id)
        {
            return BadRequest();
        }

        try
        {
            var existingNutrient = await _bll.NutrientService.FindAsync(id);
            if (existingNutrient == null)
            {
                return NotFound();
            }
            
            var nutrientBll = _mapper.Map(nutrient);
            var editedNutrient = _bll.NutrientService.Update(nutrientBll!);
            await _bll.SaveChangesAsync();
            return Ok(editedNutrient);
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }
    }
}