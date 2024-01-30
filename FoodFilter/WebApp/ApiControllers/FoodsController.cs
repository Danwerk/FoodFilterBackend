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
/// Foods Controller
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class FoodsController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly FoodMapper _mapper;

    public FoodsController(IAppBLL bll, IMapper autoMapper)
    {
        _bll = bll;
        _mapper = new FoodMapper(autoMapper);
    }


    /// <summary>
    /// Save food with images
    /// </summary>
    /// <returns></returns>
    // GET: api/Restaurants
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Restaurant>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Food>>> CreateFood([FromForm] Food food,
        [FromForm] List<IFormFile> images)
    {
        var foodBll = _mapper.Map(food);
        if (foodBll != null)
        {
            await _bll.FoodService.AddFoodWithImagesAsync(foodBll, images);
            return Ok();
        }

        return BadRequest();
    }
}