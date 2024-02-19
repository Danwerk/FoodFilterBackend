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
/// Allergens Controller
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AllergensController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly AllergenMapper _mapper;
    
    /// <summary>
    /// Foods Constructor
    /// </summary>
    /// <param name="bll">Application Business Logic Layer Interface</param>
    /// <param name="autoMapper">Auto Mapper</param>
    public AllergensController(IAppBLL bll, IMapper autoMapper)
    {
        _bll = bll;
        _mapper = new AllergenMapper(autoMapper);
        
    }
    
    
    /// <summary>
    /// Get list of all Allergens
    /// </summary>
    /// <returns>Collection of allergens</returns>
    /// <response code="200">Allergens were successfully retrieved.</response>
    /// <response code="401">Unauthorized - unable to get the data.</response>
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Allergen>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Food>>> GetAllergens()
    {
        var vm = await _bll.AllergenService.AllAsync();
        var res = vm.Select(e => _mapper.Map(e))
            .ToList();
        return Ok(res);
    }
    
    
    /// <summary>
    /// Get Allergen by ID
    /// </summary>
    /// <param name="id">Allergen ID</param>
    /// <returns>Allergen object</returns>
    [HttpGet("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Allergen>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<App.Public.DTO.v1.Allergen>> GetAllergen(Guid id)
    {
        var allergen = await _bll.AllergenService.FindAsync(id);

        if (allergen == null)
        {
            return NotFound();
        }

        var res = _mapper.Map(allergen);
        return Ok(res);
    }
    
    
    /// <summary>
    /// Update Allergen with specified id
    /// </summary>
    /// <param name="id">Allergen ID</param>
    /// <param name="allergen">Edited Allergen object that need to be updated</param>
    /// <returns>Action result</returns>
    [HttpPut("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ActionResult<App.Public.DTO.v1.Allergen>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Allergen>> EditAllergen(Guid id, Allergen allergen)
    {
        if (id != allergen.Id)
        {
            return BadRequest();
        }

        try
        {
            var allergenBll = _mapper.Map(allergen);
            var editedAllergen = _bll.AllergenService.Update(allergenBll!);
            await _bll.SaveChangesAsync();
            return Ok(editedAllergen);
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }
    }
    
    
    /// <summary>
    /// Create new Allergen
    /// </summary>
    /// <param name="allergen">New Allergen object</param>
    /// <returns>Created Allergen object</returns>
    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Allergen>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<App.Public.DTO.v1.Allergen>> CreateAllergen(Allergen allergen)
    {
        var bllAllergen = _mapper.Map(allergen);
        _bll.AllergenService.Add(bllAllergen!);
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetAllergen", new { id = allergen.Id }, allergen);
    }
    
    
    /// <summary>
    /// Delete Allergen with specified id
    /// </summary>
    /// <param name="id">Allergen ID</param>
    /// <returns>Action result</returns>
    [HttpDelete("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteAllergen(Guid id)
    {
        var allergen = await _bll.AllergenService.FirstOrDefaultAsync(id);

        if (allergen == null)
        {
            return NotFound();
        }
        await _bll.AllergenService.RemoveAsync(allergen.Id);
        await _bll.SaveChangesAsync();

        return Ok();
    }
}