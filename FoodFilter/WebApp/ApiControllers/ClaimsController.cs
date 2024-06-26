﻿using System.Net.Mime;
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
/// Claims Controller
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ClaimsController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly ClaimMapper _mapper;
    
    // <summary>
    /// Claims Constructor
    /// </summary>
    /// <param name="bll">Application Business Logic Layer Interface</param>
    /// <param name="autoMapper">Auto Mapper</param>
    public ClaimsController(IAppBLL bll, IMapper autoMapper)
    {
        _bll = bll;
        _mapper = new ClaimMapper(autoMapper);
    }
    
    /// <summary>
    /// Get list of all food Claims
    /// </summary>
    /// <returns>Collection of food claims</returns>
    /// <response code="200">Claims were successfully retrieved.</response>
    /// <response code="401">Unauthorized - unable to get the data.</response>
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<Claim>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Claim>>> GetClaims()
    {
        var vm = await _bll.ClaimService.AllAsync();
        var res = vm.Select(e => _mapper.Map(e))
            .ToList();
        return Ok(res);
    }
}