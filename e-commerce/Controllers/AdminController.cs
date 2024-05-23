using ecommerce.Business.Dto;
using ecommerce.Business.Service;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService service;

        public AdminController(IAdminService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Adds a new Admin using the data provided in the request body.
        /// </summary>
        /// <param name="dto">The data of the Admin to add.</param>
        /// <returns>
        /// Returns an HTTP 201 Created response if the Admin is successfully added,
        /// a problematic validation response in case of validation error,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<AdminDto>> Add([FromBody] AdminDto dto)
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
        /// Retrieves the details of a Admin based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the Admin to retrieve.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the Admin does not exist,
        /// the details of the Admin if found,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpGet("get/{id}")]
        public async Task<ActionResult<AdminDto>> Get(Guid id)
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
        /// Updates the details of a Admin based on its identifier using the provided data.
        /// </summary>
        /// <param name="id">The identifier of the Admin to update.</param>
        /// <param name="dto">The new data of the Admin.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the Admin does not exist,
        /// a problematic validation response in case of validation error,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<AdminDto>> Update(string id, AdminDto dto)
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
        /// Updates the password of a User based on its identifier using the provided data.
        /// </summary>
        /// <param name="id">The identifier of the User to update.</param>
        /// <param name="dto">The new data of the User.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the User does not exist,
        /// a problematic validation response in case of validation error,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpPut("update-password/{id}")]
        public async Task<ActionResult<AdminDto>> UpdatePassword(string id, AdminDto dto)
        {
            try
            {
                return await this.service.UpdatePassword(dto);
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
        /// Deletes a Admin based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the Admin to delete.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the Admin does not exist,
        /// an HTTP 200 OK response if the Admin is successfully deleted,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
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
        public ActionResult<List<AdminDto>> GetAll()
        {
            try
            {
                return this.service.GetAll();
            }
            catch (Exception e)
            {
                return this.StatusCode(500, "Internal Server Error: " + e.Message);
            }
        }

        [HttpPost("check-connection")]
        public async Task<ActionResult<AdminDto>> CheckConnection([FromBody] LoginDto dto)
        {
            try 
            {
                return await this.service.CheckConnection(dto);
            }
            catch (InvalidOperationException e)
            {
                return this.NotFound(e.Message);
            }
            catch (ArgumentNullException e)
            {
                return this.BadRequest(e.Message);
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Internal Server Error");
            }
        }
    }
}