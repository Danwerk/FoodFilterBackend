using System.Net.Mime;
using App.BLL.Services.Identity;
using App.Common;
using App.Domain.Identity;
using App.Public.DTO;
using App.Public.DTO.Mappers;
using App.Public.DTO.v1;
using Asp.Versioning;
using AutoMapper;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers.identity;

/// <summary>
/// Account Controller
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

public class UserManagerController : ControllerBase
{
    private readonly UserMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly IdentityBLL _identityBll;
    private readonly ILogger<AccountController> _logger;
    private readonly RoleManager<AppRole> _roleManager;
    
        
    public UserManagerController(UserManager<AppUser> userManager, 
        IMapper autoMapper, 
        IdentityBLL identityBll,
        ILogger<AccountController> logger, 
        RoleManager<AppRole> roleManager)
    {
        _mapper = new UserMapper(autoMapper);
        _userManager = userManager;
        _identityBll = identityBll;
        _logger = logger;
        _roleManager = roleManager;
    }

    /// <summary>
    /// Get user by email
    /// </summary>
    /// <returns>User object</returns>
    // GET: api/getUser
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.User>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUser(string email)
    {
        var user = await _identityBll.UserService.GetUser(email);
        
        var res = _mapper.Map(user);
        
        return Ok(res);
    }
        
    
    /// <summary>
    /// Get current authorized user
    /// </summary>
    /// <returns>User object</returns>
    // GET: api/getUser
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.User>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetCurrentAuthorizedUser()
    {
        var userId = User.GetUserId();
        var user = await _identityBll.UserService.GetCurrentAuthorizedUser(User);

        var res = _mapper.Map(user);
        return Ok(res);
    }
    
    
    /// <summary>
    /// Get list of all Restaurant accounts
    /// </summary>
    /// <returns>List of all Restaurants</returns>
    // GET: api/restaurantAccounts
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.User>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetRestaurantUsers()
    {
        var allRestaurantUsers = await _identityBll.UserService.GetRestaurantUsersAsync();
        
        var res = allRestaurantUsers.Select(e => _mapper.Map(e))
            .ToList();
        return Ok(res);
    }
    
    /// <summary>
    /// Get list of all unapproved Restaurant accounts.
    /// </summary>
    /// <returns>List of all unapproved Restaurants</returns>
    // GET: api/getUnapprovedRestaurantUsers
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.User>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUnapprovedRestaurantUsers()
    {
        var allUnapprovedRestaurantUsers = await _identityBll.UserService.GetUnapprovedRestaurantUsersAsync();

        var res = allUnapprovedRestaurantUsers.Select(e => _mapper.Map(e))
            .ToList();
        return Ok(res);
    }
    
    
    /// <summary>
    /// Add role to user
    /// </summary>
    /// <param name="userRoleDTO">User and role data</param>
    /// <returns>Action result</returns>
    /// <response code="200">Role was successfully added to user.</response>
    /// <response code="403">Not authorized to perform action.</response>
    /// <response code="404">User or role not found.</response>
    /// <response code="405">Role adding to user failed.</response>
    [ProducesResponseType(200)]
    [ProducesResponseType(403)]
    [ProducesResponseType(404)]
    [ProducesResponseType(405)]
    [Produces(MediaTypeNames.Application.Json)]
    [HttpPost]
    public async Task<ActionResult> AddUserToRole(UserRoleDTO userRoleDto)
    {
        var user = await _userManager.FindByEmailAsync(userRoleDto.Email);

        if (user == null)
        {
            _logger.LogInformation("User not found!");
            return StatusCode(404);
        }
        

        if (await _roleManager.FindByNameAsync(userRoleDto.RoleName) == null && userRoleDto.RoleName == RoleNames.Admin)
        {
            _logger.LogInformation("Role not found!");
            return StatusCode(404);
        }

        var result = await _userManager.AddToRoleAsync(user, userRoleDto.RoleName);

        if (!result.Succeeded)
        {
            _logger.LogInformation("Role can't be added!");
            return StatusCode(405);
        }

        _logger.LogInformation($"Role {userRoleDto.RoleName} added!");
        return Ok(result);
    }


}