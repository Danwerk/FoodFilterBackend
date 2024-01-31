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
/// Categories Controller
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
public class CategoriesController: ControllerBase
{
    
    private readonly IAppBLL _bll;
    private readonly CategoryMapper _mapper;
    /// <summary>
    /// Categories Constructor
    /// </summary>
    /// <param name="bll">Unit Of Work Interface</param>
    /// <param name="autoMapper">Auto Mapper</param>
    public CategoriesController(IAppBLL bll, IMapper autoMapper)
    {
        _bll = bll;
        _mapper = new CategoryMapper(autoMapper);
    }
    
    
    /// <summary>
    /// Get list of Categories
    /// </summary>
    /// <returns>List of all Categories</returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Category>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Category>>> GetCategories()
    {
        var vm = await _bll.CategoryService.AllAsync();
        
        var res = vm.Select(e => _mapper.Map(e))
            .ToList();
        return Ok(res);
    }
}