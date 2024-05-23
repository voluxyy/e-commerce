using ecommerce.Business.Dto;
using ecommerce.Business.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;

namespace ecommerce.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService service;

        public ReviewController(IReviewService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Adds a new Review using the data provided in the request body.
        /// </summary>
        /// <param name="dto">The data of the Review to add.</param>
        /// <returns>
        /// Returns an HTTP 201 Created response if the Review is successfully added,
        /// a problematic validation response in case of validation error,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<ReviewDto>> Add([FromBody] ReviewDto dto)
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
        /// Retrieves the details of a Review based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the Review to retrieve.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the Review does not exist,
        /// the details of the Review if found,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpGet("get/{id}")]
        public async Task<ActionResult<ReviewDto>> Get(int id)
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
        /// Updates the details of a Review based on its identifier using the provided data.
        /// </summary>
        /// <param name="id">The identifier of the Review to update.</param>
        /// <param name="dto">The new data of the Review.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the Review does not exist,
        /// a problematic validation response in case of validation error,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<ReviewDto>> Update(int id, ReviewDto dto)
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
        /// Deletes a Review based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the Review to delete.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the Review does not exist,
        /// an HTTP 200 OK response if the Review is successfully deleted,
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
        public ActionResult<List<ReviewDto>> GetAll()
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

        [HttpGet("get-from-product/{id}")]
        public ActionResult<List<ReviewDto>> GetFromProduct(int id) {
            if (id <= default(int))
            {
                return NotFound();
            }

            try {
                return this.service.GetFromProduct(id);
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("get-average-rate/{id}")]
        public ActionResult<RateDto> GetAverageRate(int id) {
            if (id <= default(int))
            {
                return NotFound();
            }

            try {
                return this.service.GetAverageRate(id);
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Internal Server Error");
            }
        }
    }
}