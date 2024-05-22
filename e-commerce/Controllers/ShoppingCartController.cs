using ecommerce.Business.Dto;
using ecommerce.Business.Service;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService service;

        public ShoppingCartController(IShoppingCartService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Adds a new ShoppingCart using the data provided in the request body.
        /// </summary>
        /// <param name="dto">The data of the ShoppingCart to add.</param>
        /// <returns>
        /// Returns an HTTP 201 Created response if the ShoppingCart is successfully added,
        /// a problematic validation response in case of validation error,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<ShoppingCartDto>> Add([FromBody] ShoppingCartDto dto)
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
        /// Retrieves the details of a ShoppingCart based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the ShoppingCart to retrieve.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the ShoppingCart does not exist,
        /// the details of the ShoppingCart if found,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpGet("get/{id}")]
        public async Task<ActionResult<ShoppingCartDto>> Get(int id)
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

        [HttpGet("get-from-user/{id}")]
        public async Task<ActionResult<ShoppingCartDto>> GetFromUser(int id)
        {
            if (id <= default(int))
            {
                return NotFound();
            }

            try
            {
                return await this.service.GetFromUser(id);
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Updates the details of a ShoppingCart based on its identifier using the provided data.
        /// </summary>
        /// <param name="id">The identifier of the ShoppingCart to update.</param>
        /// <param name="dto">The new data of the ShoppingCart.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the ShoppingCart does not exist,
        /// a problematic validation response in case of validation error,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<ShoppingCartDto>> Update(int id, ShoppingCartDto dto)
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
        /// Deletes a ShoppingCart based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the ShoppingCart to delete.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the ShoppingCart does not exist,
        /// an HTTP 200 OK response if the ShoppingCart is successfully deleted,
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
        public ActionResult<List<ShoppingCartDto>> GetAll()
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