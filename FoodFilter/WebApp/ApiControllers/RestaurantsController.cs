using System.Net.Mime;
using App.Common;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
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

        private const int MaxImageUploadCount = 5; // Maximum number of images allowed per upload
        private const long MaxImageSize = 10 * 1024 * 1024; // Maximum allowed size for each image (10 MB)


        /// <summary>
        /// Restaurants Constructor
        /// </summary>
        /// <param name="bll">Application Business Logic Layer Interface</param>
        /// <param name="autoMapper">Auto Mapper</param>
        public RestaurantsController(IAppBLL bll, IMapper autoMapper, IFileService fileService)
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
        [ProducesResponseType(typeof(IEnumerable<Restaurant>), StatusCodes.Status200OK)]
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
        /// Get list of all Restaurants for regular user based on search
        /// </summary>
        /// <returns>Collection of restaurants</returns>
        /// <response code="200">Restaurants were successfully retrieved.</response>
        /// <response code="401">Unauthorized - unable to get the data.</response>
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<Restaurant>), StatusCodes.Status200OK)]
        [HttpGet("{limit}/{search?}")]
        [AllowAnonymous]
        public Task<ActionResult<IEnumerable<Restaurant>>> GetAllRestaurants(int limit, string? search)
        {
            var vm = _bll.RestaurantService.GetAll(limit, search);
            var res = vm.Select(e => _mapper.Map(e))
                .ToList();
            return Task.FromResult<ActionResult<IEnumerable<Restaurant>>>(Ok(res));
        }

        /// <summary>
        /// Get restaurants that are unapproved by system administrator.
        /// </summary>
        /// <returns>Collection of restaurants</returns>
        /// <response code="200">Unapproved restaurants were successfully retrieved.</response>
        /// <response code="401">Unauthorized - unable to get the data.</response>
        /// <response code="404">Not found data.</response>
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<Restaurant>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetUnapprovedRestaurants()
        {
            var vm = await _bll.RestaurantService.GetUnapprovedRestaurants();
            if (vm != null)
            {
                var res = vm.Select(e => _mapper.Map(e))
                    .ToList();
                return Ok(res);
            }

            return NotFound();
        }


        /// <summary>
        /// Get restaurants that are approved by system administrator.
        /// </summary>
        /// <returns>Collection of restaurants</returns>
        /// <response code="200">Approved restaurants were successfully retrieved.</response>
        /// <response code="401">Unauthorized - unable to get the data.</response>
        /// <response code="404">Not found data.</response>
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<Restaurant>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetApprovedRestaurants()
        {
            var vm = await _bll.RestaurantService.GetApprovedRestaurants();


            if (vm != null)
            {
                var res = vm.Select(e => _mapper.Map(e))
                    .ToList();
                return Ok(res);
            }

            return NotFound();
        }


        /// <summary>
        /// Get restaurants that are in pending state and waiting for approval by system administrator.
        /// </summary>
        /// <returns>Collection of restaurants</returns>
        /// <response code="200">Pending restaurants were successfully retrieved.</response>
        /// <response code="401">Unauthorized - unable to get the data.</response>
        /// <response code="404">Not found data.</response>
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<Restaurant>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = RoleNames.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetPendingRestaurants()
        {
            var vm = await _bll.RestaurantService.GetPendingRestaurants();
            if (vm != null)
            {
                var res = vm.Select(e => _mapper.Map(e))
                    .ToList();
                return Ok(res);
            }

            return NotFound();
        }

        /// <summary>
        /// Get restaurant users that are expired, it means that account payment has expired.
        /// </summary>
        /// <returns>Collection of restaurants</returns>
        /// <response code="200">Pending restaurants were successfully retrieved.</response>
        /// <response code="401">Unauthorized - unable to get the data.</response>
        /// <response code="404">Not found data.</response>
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<Restaurant>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetExpiredRestaurants()
        {
            var vm = await _bll.RestaurantService.GetExpiredRestaurants();
            if (vm != null)
            {
                var res = vm.Select(e => _mapper.Map(e))
                    .ToList();
                return Ok(res);
            }

            return NotFound();
        }

        /// <summary>
        /// Approve restaurant, whose profile is overviewed by system administrator.
        /// </summary>
        /// <param name="id">Restaurant id</param>
        /// <returns>Approved restaurant object</returns>
        /// <response code="200">Restaurant was successfully approved.</response>
        /// <response code="401">Unauthorized - unable to perform this action.</response>
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Restaurant), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = RoleNames.Admin)]
        [HttpPost("{id}")]
        public async Task<ActionResult<Restaurant>> ApproveRestaurant(Guid id)
        {
            var restaurant = await _bll.RestaurantService.ApproveRestaurantAsync(id);

            return Ok(restaurant);
        }


        /// <summary>
        /// Disapprove restaurant, whose profile does not meet the requirements.
        /// </summary>
        /// <param name="id">Restaurant id</param>
        /// <returns>Disapproved restaurant object</returns>
        /// <response code="200">Restaurant was successfully disapproved.</response>
        /// <response code="401">Unauthorized - unable to perform this action.</response>
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Restaurant), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = RoleNames.Admin)]
        [HttpPost("{id}")]
        public async Task<ActionResult<Restaurant>> DisapproveRestaurant(Guid id)
        {
            var restaurant = await _bll.RestaurantService.DisapproveRestaurantAsync(id);

            return Ok(restaurant);
        }

        /// <summary>
        /// Get Restaurant by Restaurant ID
        /// </summary>
        /// <param name="id">Restaurant ID</param>
        /// <returns>Restaurant object</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Restaurant), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        /// Get Restaurant for currently logged in user
        /// </summary>
        /// <returns>Restaurant object</returns>
        /// <response code="200">Restaurant was successfully retrieved.</response>
        /// <response code="401">Unauthorized - unable to perform this action.</response>
        /// <response code="404">Not found</response>
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Restaurant), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
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
        /// Get Restaurant by user id
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>Restaurant object</returns>
        /// <response code="200">Restaurant was successfully retrieved.</response>
        /// <response code="401">Unauthorized - unable to perform this action.</response>
        /// <response code="404">Not found</response>
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Restaurant), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(RestApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurantByUserId(Guid id)
        {
            var restaurant = await _bll.RestaurantService.GetRestaurant(id);

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
        [HttpPut("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Restaurant), StatusCodes.Status204NoContent)]
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


            if (restaurantEditDto.RestaurantAllergens != null)
            {
                var existingRestaurantAllergens = await _bll.RestaurantAllergenService.AllAsync(id);
                foreach (var allergen in existingRestaurantAllergens)   
                {
                    if (restaurantEditDto.RestaurantAllergens.Any(r => r.AllergenId == allergen.AllergenId))
                    {
                        await _bll.RestaurantAllergenService.RemoveAsync(allergen.Id);
                    }
                }
            }

            restaurantEditDto.PaymentStartsAt = restaurant.PaymentStartsAt;
            restaurantEditDto.PaymentEndsAt = restaurant.PaymentEndsAt;

            restaurant = _mapper.Map(restaurantEditDto);
            restaurant!.AppUserId = userId;


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


        /// <summary>
        /// Confirm restaurant payment.
        /// </summary>
        /// <param name="id">Restaurant id</param>
        /// <returns>Confirmed restaurant payment object</returns>
        /// <response code="200">Restaurant payment was successfully confirmed.</response>
        /// <response code="401">Unauthorized - unable to get the data.</response>
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Restaurant), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = RoleNames.Admin)]
        [HttpPost("{id}")]
        public async Task<ActionResult<Restaurant>> ConfirmRestaurantPayment(Guid id)
        {
            var restaurant = await _bll.RestaurantService.ConfirmRestaurantPaymentAsync(id);

            return Ok(restaurant);
        }


        /// <summary>
        /// Upload images.
        /// </summary>
        /// <param name="id">Restaurant id</param>
        /// <param name="images">Restaurant images</param>
        /// <returns></returns>
        /// <response code="204">Restaurant image was successfully uploaded.</response>
        /// <response code="401">Unauthorized - unable to do this action.</response>
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Restaurant), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = RoleNames.Restaurant)]
        [HttpPost("{id}")]
        public async Task<ActionResult> UploadImages(Guid id, [FromForm] List<IFormFile> images)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid restaurant ID");
            }

            var restaurant = await _bll.RestaurantService.FindAsync(id);
            if (restaurant == null)
            {
                return BadRequest("Missing restaurant with such id, " + id);
            }

            if (images == null || images.Count == 0)
            {
                return BadRequest("No images provided");
            }

            if (images.Count + restaurant.Images!.Count > MaxImageUploadCount)
            {
                return BadRequest($"Exceeded maximum allowed number of images ({MaxImageUploadCount})");
            }

            foreach (var image in images)
            {
                if (image.Length == 0)
                {
                    return BadRequest("Empty image file(s) provided");
                }

                if (image.Length > MaxImageSize)
                {
                    return BadRequest($"Image file size exceeds the maximum allowed size ({MaxImageSize} bytes)");
                }
            }

            try
            {
                await _bll.RestaurantService.UploadRestaurantImagesAsync(id, images);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to upload images.");
            }
        }


        /// <summary>
        /// Upload images.
        /// </summary>
        /// <param name="id">Image id</param>
        /// <returns></returns>
        /// <response code="200">Image was successfully deleted.</response>
        /// <response code="401">Unauthorized - unable to delete the data.</response>
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Restaurant), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = RoleNames.Restaurant)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteImage(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid image ID");
            }

            var image = await _bll.ImageService.GetImage(id);
            if (image == null)
            {
                return NotFound("Image with such id not found, " + id);
            }

            try
            {
                await _bll.FileService.DeleteImageFromFileSystemAsync(image.Url!);
                await _bll.ImageService.RemoveAsync(image.Id);
                await _bll.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete image.");
            }
        }


        [HttpGet("{restaurantId}")]
        public async Task<ActionResult> GetRestaurantAllergens(Guid restaurantId)
        {
            var restaurantAllergens = await _bll.RestaurantAllergenService.AllAsync(restaurantId);

            if (restaurantAllergens == null || !restaurantAllergens.Any())
            {
                return NotFound();
            }

            return Ok(restaurantAllergens);
        }
    }
}