using System.Globalization;
using System.Net.Mime;
using App.Contracts.BLL;
using App.Public.DTO.v1;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using App.Common.OpenHoursDtos;
using App.Public.DTO.Mappers;

namespace WebApp.ApiControllers;

/// <summary>
/// OpenHours Controller
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class OpenHoursController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly OpenHoursMapper _mapper;

    /// <summary>
    /// OpenHours Constructor
    /// </summary>
    /// <param name="bll">Application Business Logic Layer Interface</param>
    /// <param name="autoMapper">Auto Mapper</param>
    public OpenHoursController(IAppBLL bll, IMapper autoMapper)
    {
        _bll = bll;
        _mapper = new OpenHoursMapper(autoMapper);
    }

    /// <summary>
    /// Get list of OpenHours for restaurant
    /// </summary>
    /// <param name="restaurantId">Restaurant id for which to get open hours.</param>
    /// <returns>OpenHours objects</returns>
    /// <response code="200">OpenHours was successfully retrieved.</response>
    /// <response code="401">Not authorized to perform action.</response>
    [ProducesResponseType(typeof(Task<ActionResult<Restaurant>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces(MediaTypeNames.Application.Json)]
    [HttpGet("{restaurantId}")]
    public async Task<ActionResult<List<OpenHoursDTO>>> GetOpeningHours(Guid restaurantId)
    {
        var vm = await _bll.OpenHoursService.GetOpeningHoursForRestaurant(restaurantId);

        var res = vm.Select(e => new OpenHoursDTO
        {
            Day = e.Day,
            Open = e.Open.ToString("hh\\:mm"),
            Close = e.Close.ToString("hh\\:mm")
        }).ToList();

        return Ok(res);
    }

    /// <summary>
    /// Save new OpenHours for restaurant
    /// </summary>
    /// <param name="request">OpenHours request dto</param>
    /// <returns>Created OpenHours object</returns>
    /// <response code="201">OpenHours was successfully created.</response>
    /// <response code="401">Not authorized to perform action.</response>
    [ProducesResponseType(typeof(Task<ActionResult<Restaurant>>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces(MediaTypeNames.Application.Json)]
    [HttpPost]
    public async Task<ActionResult<List<OpenHours>>> SaveOpeningHours(OpenHours openHours)
    {
        var openHoursEntities = new List<OpenHours>();
        if (openHours != null)
        {
            // var open = TimeSpan.ParseExact(request.Open, "hh\\:mm", CultureInfo.InvariantCulture);
            // var close = TimeSpan.ParseExact(request.Close, "hh\\:mm", CultureInfo.InvariantCulture);
            var open = openHours.Open;
            var close = openHours.Close;

            var openHoursEntity = new OpenHours()
            {
                RestaurantId = openHours.RestaurantId,
                Day = openHours.Day,
                Open = open,
                Close = close
            };

            openHoursEntities.Add(openHoursEntity);


            var openHoursBllEntities = openHoursEntities.Select(r => _mapper.Map(r)).ToList();

            await _bll.OpenHoursService.SaveOpenHours(openHoursBllEntities);

            return Ok(openHoursEntities);
        }

        return BadRequest("No opening hours provided.");
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
    public async Task<ActionResult> DeleteOpenHours(Guid id)
    {
        var openHours = await _bll.OpenHoursService.FirstOrDefaultAsync(id);

        if (openHours == null)
        {
            return NotFound();
        }

        await _bll.OpenHoursService.RemoveAsync(openHours.Id);
        await _bll.SaveChangesAsync();

        return Ok();
    }
}