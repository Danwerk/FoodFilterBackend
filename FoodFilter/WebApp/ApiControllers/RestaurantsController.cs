using System.Net.Mime;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Public.DTO.Mappers;
using App.Public.DTO.v1;
using Asp.Versioning;
using AutoMapper;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Restaurant = App.Public.DTO.v1.Restaurant;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Restaurants Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RestaurantsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly RestaurantMapper _mapper;

        /// <summary>
        /// Restaurants Constructor
        /// </summary>
        /// <param name="bll">Application Business Logic Layer Interface</param>
        /// <param name="autoMapper">Auto Mapper</param>
        public RestaurantsController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new RestaurantMapper(autoMapper);
        }


        /// <summary>
        /// Get list of all Restaurants
        /// </summary>
        /// <returns>Collection of restaurants</returns>
        /// <response code="200">Restaurants were successfully retrieved.</response>
        /// <response code="401">Unauthorized - unable to get the data.</response>
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Restaurant>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            var vm = await _bll.RestaurantService.AllAsync();
            var res = vm.Select(e => _mapper.Map(e))
                .ToList();
            return Ok(res);
        }
        
        
        /// <summary>
        /// Get restaurant users that are unapproved by system administrator.
        /// </summary>
        /// <returns>Collection of restaurants</returns>
        /// <response code="200">Unapproved restaurants were successfully retrieved.</response>
        /// <response code="401">Unauthorized - unable to get the data.</response>
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Restaurant>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetUnapprovedRestaurants()
        {
            var vm = await _bll.RestaurantService.GetUnapprovedRestaurants();
            var res = vm.Select(e => _mapper.Map(e))
                .ToList();
            return Ok(res);
        }
        
        
        /// <summary>
        /// Get restaurant users that are approved by system administrator.
        /// </summary>
        /// <returns>Collection of restaurants</returns>
        /// <response code="200">Approved restaurants were successfully retrieved.</response>
        /// <response code="401">Unauthorized - unable to get the data.</response>
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Restaurant>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetApprovedRestaurants()
        {
            var vm = await _bll.RestaurantService.GetApprovedRestaurants();
            var res = vm.Select(e => _mapper.Map(e))
                .ToList();
            return Ok(res);
        }
        
        
        /// <summary>
        /// Approve restaurant, whose profile is overviewed by system administrator.
        /// </summary>
        /// <param name="id">Restaurant id</param>
        /// <returns>Approved restaurant object</returns>
        /// <response code="200">Restaurant was successfully approved.</response>
        /// <response code="401">Unauthorized - unable to get the data.</response>
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Restaurant>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost ("{id}")]
        public async Task<ActionResult<Restaurant>> ApproveRestaurant(Guid id)
        {
            var restaurant = await _bll.RestaurantService.ApproveRestaurantAsync(id);

            return Ok(restaurant);
        }

         // GET: api/Restaurants/5
         [HttpGet("{id}")]
         public async Task<ActionResult<Restaurant>> GetRestaurant(Guid id)
         {
             var restaurant = await _bll.RestaurantService.FindAsync(id);

             if (restaurant == null)
             {
                 return NotFound();
             }

             var res = _mapper.Map(restaurant);
             return Ok(res);
         }
         
         
         // GET: api/GetRestaurantForCurrentUser
         [Produces(MediaTypeNames.Application.Json)]
         [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Restaurant>), StatusCodes.Status200OK)]
         [ProducesResponseType(StatusCodes.Status401Unauthorized)]
         [HttpGet]
         public async Task<ActionResult<Restaurant>> GetRestaurantForCurrentUser()
         {
             var userId = User.GetUserId();
             var restaurant = await _bll.RestaurantService.GetRestaurant(userId);

             if (restaurant == null)
             {
                 return NotFound();
             }

             var res = _mapper.Map(restaurant);
             return Ok(res);
         }

        /// <summary>
        /// Update Restaurant with specified id
        /// </summary>
        /// <param name="id">Restaurant ID</param>
        /// <param name="restaurantEditDto">Edited Restaurant object that need to be updated</param>
        /// <returns>Action result</returns>
        // PUT: api/Restaurants/5
        [HttpPut("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Restaurant>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditRestaurant(Guid id, RestaurantEdit restaurantEditDto)
        {
            if (id != restaurantEditDto.Id)
            {
                return BadRequest();
            }

            var restaurant = await _bll.RestaurantService.FirstOrDefaultAsync(id);
            if (restaurant == null)
            {
                return BadRequest();
            }

            var userId = restaurant.AppUserId;

            restaurant = _mapper.MapRestaurantEdit(restaurantEditDto);
            restaurant.AppUserId = userId;

            try
            {
                await _bll.RestaurantService.Edit(restaurant);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Create new restaurant with Id and UserId
        /// </summary>
        /// <param name="restaurantCreateDto">Restaurant to create</param>
        /// <returns>Created restaurant object</returns>
        /// <response code="201">Restaurant was successfully created.</response>
        /// <response code="401">Not authorized to perform action.</response>
        [ProducesResponseType(typeof(Task<ActionResult<Restaurant>>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces(MediaTypeNames.Application.Json)]
        [HttpPost]
        public async Task<ActionResult<Restaurant>> CreateRestaurant(RestaurantCreate restaurantCreateDto)
        {
            // todo: restaurant should be created for user. Refactor register method, restaurant creation should not be done there.
            
            restaurantCreateDto.Id = Guid.NewGuid();
           
            var restaurant = _mapper.MapRestaurantCreate(restaurantCreateDto);

            _bll.RestaurantService.Add(restaurant);
            await _bll.SaveChangesAsync();
            
            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }
    }
}
//         // DELETE: api/Restaurants/5
//         [HttpDelete("{id}")]
//         public async Task<IActionResult> DeleteRestaurant(Guid id)
//         {
//             if (_context.Restaurants == null)
//             {
//                 return NotFound();
//             }
//             var restaurant = await _context.Restaurants.FindAsync(id);
//             if (restaurant == null)
//             {
//                 return NotFound();
//             }
//
//             _context.Restaurants.Remove(restaurant);
//             await _context.SaveChangesAsync();
//
//             return NoContent();
//         }
//
//         private bool RestaurantExists(Guid id)
//         {
//             return (_context.Restaurants?.Any(e => e.Id == id)).GetValueOrDefault();
//         }
//     }
// }