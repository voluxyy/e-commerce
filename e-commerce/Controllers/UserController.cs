using ecommerce.Business.Dto;
using ecommerce.Business.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ecommerce.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;

        public UserController(IUserService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Adds a new User using the data provided in the request body.
        /// </summary>
        /// <param name="dto">The data of the User to add.</param>
        /// <returns>
        /// Returns an HTTP 201 Created response if the User is successfully added,
        /// a problematic validation response in case of validation error,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<UserDto>> Add()
        {
            try
            {
                var formCollection = await this.Request.ReadFormAsync();

                var jsonDto = formCollection["dto"];
                var dto = JsonConvert.DeserializeObject<UserDto>(jsonDto!);

                Console.WriteLine(dto);

                await this.service.Add(dto!);
                return StatusCode(StatusCodes.Status201Created, dto);
            }
            catch (ArgumentNullException)
            {
                return this.ValidationProblem();
            }
            catch (Exception e)
            {
                return this.StatusCode(500, "Internal Server Error: "+e.Message);
            }
        }

        /// <summary>
        /// Retrieves the details of a User based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the User to retrieve.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the User does not exist,
        /// the details of the User if found,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpGet("get/{id}")]
        public async Task<ActionResult<UserDto>> Get(int id)
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
        /// Updates the details of a User based on its identifier using the provided data.
        /// </summary>
        /// <param name="id">The identifier of the User to update.</param>
        /// <param name="dto">The new data of the User.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the User does not exist,
        /// a problematic validation response in case of validation error,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<UserDto>> Update(int id, UserDto dto)
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
        public async Task<ActionResult<UserDto>> UpdatePassword(int id, UserDto dto)
        {
            if (id <= default(int))
            {
                return NotFound();
            }

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

        [HttpPut("update-money/{id}")]
        public async Task<ActionResult<UserDto>> UpdateMoney(int id, MoneyDto dto)
        {
            if (id <= default(int))
            {
                return NotFound();
            }

            try
            {
                return await this.service.UpdateMoney(dto);
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
        /// Deletes a User based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the User to delete.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the User does not exist,
        /// an HTTP 200 OK response if the User is successfully deleted,
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
        public ActionResult<List<UserDto>> GetAll()
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

        [HttpPost("check-connection")]
        public async Task<ActionResult<UserDto>> CheckConnection([FromBody] LoginDto dto)
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