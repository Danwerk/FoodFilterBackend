using System.Net.Mime;
using App.Contracts.BLL;
using App.Public.DTO.Mappers;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    /// Ingredients Constructor
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
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Nutrient>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Nutrient>>> GetNutrients()
    {
        var vm = await _bll.NutrientService.AllAsync();
        var res = vm.Select(e => _mapper.Map(e))
            .ToList();
        return Ok(res);
    }
}