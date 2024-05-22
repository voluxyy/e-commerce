using ecommerce.Business.Dto;
using ecommerce.Business.Service;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ProductListController : ControllerBase
    {
        private readonly IProductListService service;

        public ProductListController(IProductListService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Adds a new ProductList using the data provided in the request body.
        /// </summary>
        /// <param name="dto">The data of the ProductList to add.</param>
        /// <returns>
        /// Returns an HTTP 201 Created response if the ProductList is successfully added,
        /// a problematic validation response in case of validation error,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<ProductListDto>> Add([FromBody] ProductListDto dto)
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
        /// Retrieves the details of a ProductList based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the ProductList to retrieve.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the ProductList does not exist,
        /// the details of the ProductList if found,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpGet("get/{id}")]
        public async Task<ActionResult<ProductListDto>> Get(int id)
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

        [HttpGet("get-from-shopping-cart/{id}")]
        public async Task<ActionResult<List<ProductListDto>>> GetFromShoppingCart(int id)
        {
            if (id <= default(int))
            {
                return NotFound();
            }

            try
            {
                return await this.service.GetFromShoppingCart(id);
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Updates the details of a ProductList based on its identifier using the provided data.
        /// </summary>
        /// <param name="id">The identifier of the ProductList to update.</param>
        /// <param name="dto">The new data of the ProductList.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the ProductList does not exist,
        /// a problematic validation response in case of validation error,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<ProductListDto>> Update(int id, ProductListDto dto)
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
        /// Deletes a ProductList based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the ProductList to delete.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the ProductList does not exist,
        /// an HTTP 200 OK response if the ProductList is successfully deleted,
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

        [HttpDelete("delete-from-product/{id}")]
        public async Task<IActionResult> DeleteFromProduct(int id)
        {
            if (id <= default(int))
            {
                return NotFound();
            }

            try
            {
                await this.service.DeleteFromProduct(id);
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
        public ActionResult<List<ProductListDto>> GetAll()
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