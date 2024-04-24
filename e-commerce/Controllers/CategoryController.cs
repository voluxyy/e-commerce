using ecommerce.Business.Dto;
using ecommerce.Business.Service;
using ecommerce.Business.Service.Interface;
using ecommerce.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService service;

        public CategoryController(ICategoryService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Adds a new Category using the data provided in the request body.
        /// </summary>
        /// <param name="dto">The data of the Category to add.</param>
        /// <returns>
        /// Returns an HTTP 201 Created response if the Category is successfully added,
        /// a problematic validation response in case of validation error,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Add([FromBody] CategoryDto dto)
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
        /// Retrieves the details of a Category based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the Category to retrieve.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the Category does not exist,
        /// the details of the Category if found,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpGet("get/{id}")]
        public async Task<ActionResult<CategoryDto>> Get(int id)
        {
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
        /// Updates the details of a Category based on its identifier using the provided data.
        /// </summary>
        /// <param name="id">The identifier of the Category to update.</param>
        /// <param name="dto">The new data of the Category.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the Category does not exist,
        /// a problematic validation response in case of validation error,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<CategoryDto>> Update(int id, CategoryDto dto)
        {
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
        /// Deletes a Category based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the Category to delete.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the Category does not exist,
        /// an HTTP 200 OK response if the Category is successfully deleted,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
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
        public ActionResult<List<CategoryDto>> GetAll()
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
