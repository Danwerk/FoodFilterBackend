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
    
    /// <summary>
    /// Foods Constructor
    /// </summary>
    /// <param name="bll">Application Business Logic Layer Interface</param>
    /// <param name="autoMapper">Auto Mapper</param>
    public FoodsController(IAppBLL bll, IMapper autoMapper)
    {
        _bll = bll;
        _mapper = new FoodMapper(autoMapper);
    }

    
    /// /// <summary>
    /// Save food with images
    /// </summary>
    /// <param name="food">Food object</param>
    /// <param name="images">Images that should be saved</param>
    /// <returns>Action result</returns>
    // GET: api/Restaurants
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Food>), StatusCodes.Status200OK)]
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
    
    
    /// <summary>
    /// Get Food by id
    /// </summary>
    /// <returns>Food object</returns>
    /// <response code="200">Food object were successfully retrieved.</response>
    /// <response code="401">Unauthorized - unable to get the data.</response>
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Food>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("{id}")]
    public async Task<ActionResult<Food>> GetFood(Guid id)
    {
        var food = await _bll.FoodService.GetFood(id);

        if (food == null)
        {
            return NotFound();
        }

        var res = _mapper.Map(food);
        return Ok(res);
    }
    
    
    /// <summary>
    /// Get list of all Foods
    /// </summary>
    /// <returns>Collection of foods</returns>
    /// <response code="200">Foods were successfully retrieved.</response>
    /// <response code="401">Unauthorized - unable to get the data.</response>
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Food>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Food>>> GetFoods()
    {
        var vm = await _bll.FoodService.AllAsync();
        var res = vm.Select(e => _mapper.Map(e))
            .ToList();
        return Ok(res);
    }
    
    
    /// <summary>
    /// Calculate nutrition information for food.
    /// </summary>
    /// <returns>DTO of calculated result</returns>
    /// <response code="200">Food nutrients were successfully calculated.</response>
    /// <response code="401">Unauthorized - unable to get the data.</response>
    [Produces(MediaTypeNames.Application.Json)]
    // [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Food>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("{id}")]
    public async Task<ActionResult<Food>> CalculateFoodNutrition(Guid id)
    {
        var food = await _bll.FoodService.GetFood(id);
        if (food == null)
        {
            return NotFound($"Food with id {id} not found");
        }

        var foodNutrition = await _bll.FoodService.CalculateFoodNutrition(food);
        food.FoodNutrients = foodNutrition.FoodNutrients;
        
        var res = _mapper.Map(food);
        return Ok(res);
    }

}