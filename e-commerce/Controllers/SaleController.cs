using ecommerce.Business.Dto;
using ecommerce.Business.Service;
using ecommerce.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService service;

        public SaleController(ISaleService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Adds a new Sale using the data provided in the request body.
        /// </summary>
        /// <param name="dto">The data of the Sale to add.</param>
        /// <returns>
        /// Returns an HTTP 201 Created response if the Sale is successfully added,
        /// a problematic validation response in case of validation error,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<SaleDto>> Add([FromBody] SaleDto dto)
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
            catch (Exception e)
            {
                return this.StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Retrieves the details of a Sale based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the Sale to retrieve.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the Sale does not exist,
        /// the details of the Sale if found,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpGet("get/{id}")]
        public async Task<ActionResult<SaleDto>> Get(int id)
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
        public async Task<ActionResult<List<SaleDto>>> GetFromUser(int id)
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
        /// Updates the details of a Sale based on its identifier using the provided data.
        /// </summary>
        /// <param name="id">The identifier of the Sale to update.</param>
        /// <param name="dto">The new data of the Sale.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the Sale does not exist,
        /// a problematic validation response in case of validation error,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<SaleDto>> Update(int id, SaleDto dto)
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
        /// Deletes a Sale based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the Sale to delete.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the Sale does not exist,
        /// an HTTP 200 OK response if the Sale is successfully deleted,
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
        public ActionResult<List<SaleDto>> GetAll()
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

        [HttpGet("has-buy/{productId}/{userId}")]
        public async Task<ActionResult<Boolean>> HasBuy(int productId, int userId ) {
            try
            {
                HasBuy hasBuy = new HasBuy
                {
                    UserId = userId,
                    ProductId = productId,
                };
                
                return await this.service.HasBuy(hasBuy);
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("get-last-7-days")]
        public async Task<ActionResult<List<SaleDto>>> GetLast7Days()
        {
            try
            {
                return await this.service.GetLast7Days();
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("get-total-revenues-from-last-7-Days")]
        public async Task<ActionResult<float>> GetTotalRevenuesFromLast7Days()
        {
            try
            {
                return await this.service.GetTotalRevenuesFromLast7Days();
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Internal Server Error");
            }
        }
    }
}