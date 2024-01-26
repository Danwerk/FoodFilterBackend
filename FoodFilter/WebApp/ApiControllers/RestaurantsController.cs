using System.Net.Mime;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Public.DTO.Mappers;
using App.Public.DTO.v1;
using Asp.Versioning;
using AutoMapper;
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

        public RestaurantsController(IAppBLL bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new RestaurantMapper(autoMapper);
        }


        /// <summary>
        /// Get list of Restaurants
        /// </summary>
        /// <returns>List of all Units</returns>
        // GET: api/Restaurants
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

        // POST: api/Restaurants
        [HttpPost]
        public async Task<ActionResult<Restaurant>> CreateRestaurant(Restaurant restaurant)
        {
            // todo: restaurant should be created for user. Refactor register method, restaurant creation should not be done there.
            restaurant.Id = Guid.NewGuid();
            var bllRestaurant = _mapper.Map(restaurant);
            _bll.RestaurantService.Add(bllRestaurant!);
            await _bll.SaveChangesAsync();
            
            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }
    }
}
//
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