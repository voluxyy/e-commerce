using ecommerce.Business.Dto;
using ecommerce.Business.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class WishController : ControllerBase
    {
        private readonly IWishService service;

        public WishController(IWishService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Adds a new Wish using the data provided in the request body.
        /// </summary>
        /// <param name="dto">The data of the Wish to add.</param>
        /// <returns>
        /// Returns an HTTP 201 Created response if the Wish is successfully added,
        /// a problematic validation response in case of validation error,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<WishDto>> Add([FromBody] WishDto dto)
        {
            try
            {
                await this.service.Add(dto);
                return StatusCode(StatusCodes.Status201Created, dto);
            }
            catch (ArgumentNullException)
            {
                return this.ValidationProblem();
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Retrieves the details of a Wish based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the Wish to retrieve.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the Wish does not exist,
        /// the details of the Wish if found,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpGet("get/{id}")]
        public async Task<ActionResult<WishDto>> Get(int id)
        {
            if (id <= default(int))
            {
                return NotFound();
            }

            try
            {
                return await this.service.Get(id);
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Updates the details of a Wish based on its identifier using the provided data.
        /// </summary>
        /// <param name="id">The identifier of the Wish to update.</param>
        /// <param name="dto">The new data of the Wish.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the Wish does not exist,
        /// a problematic validation response in case of validation error,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<WishDto>> Update(int id, WishDto dto)
        {
            if (id <= default(int))
            {
                return NotFound();
            }

            try
            {
                return await this.service.Update(dto);
            }
            catch (ArgumentNullException)
            {
                return this.ValidationProblem();
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Deletes a Wish based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the Wish to delete.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the Wish does not exist,
        /// an HTTP 200 OK response if the Wish is successfully deleted,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= default(int))
            {
                return NotFound();
            }

            try
            {
                await this.service.Delete(id);
                return this.Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Gets the list of all categories from the service and then returns this list as a JSON object.
        /// </summary>
        /// <returns>
        /// Returns an HTTP response containing the list of categories as a JSON object,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpGet("all")]
        public ActionResult<List<WishDto>> GetAll()
        {
            try
            {
                return this.service.GetAll();
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Internal Server Error");
            }
        }
    }
}
