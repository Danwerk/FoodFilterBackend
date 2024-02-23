using System.Net.Mime;
using App.Common.NutrientCalculationDtos;
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
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Food>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost]
    public async Task<ActionResult<Food>> CreateFood([FromForm] Food food,
        [FromForm] List<IFormFile> images)
    {
        food.Id = Guid.NewGuid();
        var foodBll = _mapper.Map(food);

        if (foodBll != null)
        {
            await _bll.FoodService.AddFoodWithImagesAsync(foodBll, images);

            return CreatedAtAction("GetFood", new { id = food.Id }, food);
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
        var food = await _bll.FoodService.FindAsync(id);

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
    /// Get list of all Foods for restaurant
    /// </summary>
    /// <returns>Collection of foods</returns>
    /// <response code="200">Foods were successfully retrieved.</response>
    /// <response code="401">Unauthorized - unable to get the data.</response>
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Food>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Food>>> GetFoodsByRestaurantId(Guid id)
    {
        var vm = await _bll.FoodService.AllAsync(id);
        var res = vm.Select(e => _mapper.Map(e))
            .ToList();
        
        
        return Ok(res);
    }
    
    
    /// <summary>
    /// Update Food with specified id
    /// </summary>
    /// <param name="id">Food ID</param>
    /// <param name="food">Edited Food object that need to be updated</param>
    /// <returns>Action result</returns>
    [HttpPut("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ActionResult<App.Public.DTO.v1.Food>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Food>> EditFood(Guid id, Food food)
    {
        if (id != food.Id)
        {
            return BadRequest();
        }

        try
        {
            var foodBll = _mapper.Map(food);
            var editedFood = _bll.FoodService.Update(foodBll!);
            await _bll.SaveChangesAsync();
            return Ok(editedFood);
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }
    }
    
    
    /// <summary>
    /// Update Food with specified id
    /// </summary>
    /// <param name="id">Food ID</param>
    /// <param name="food">Edited Food object that need to be updated</param>
    /// <returns>Action result</returns>
    [HttpDelete("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteFood(Guid id)
    {
        var food = await _bll.FoodService.FirstOrDefaultAsync(id);

        if (food == null)
        {
            return NotFound();
        }
        await _bll.FoodService.RemoveAsync(food.Id);
        await _bll.SaveChangesAsync();

        return Ok();
    }
    
    /// <summary>
    /// Calculate nutrition and energy information for food.
    /// </summary>
    /// <returns>DTO of calculated result</returns>
    /// <response code="200">Food nutrients were successfully calculated.</response>
    /// <response code="401">Unauthorized - unable to get the data.</response>
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost]
    public async Task<ActionResult<FoodCalculationResultDto>> CalculateNutrition([FromBody] FoodCalculationRequestDto request)
    {
        var foodCalculationResult = await _bll.FoodService.CalculateNutrients(request);
        return Ok(foodCalculationResult);
    }
}