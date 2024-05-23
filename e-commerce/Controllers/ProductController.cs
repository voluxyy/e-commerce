using ecommerce.Business.Dto;
using ecommerce.Business.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ecommerce.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService service;

        public ProductController(IProductService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Adds a new Product using the data provided in the request body.
        /// </summary>
        /// <param name="dto">The data of the Product to add.</param>
        /// <returns>
        /// Returns an HTTP 201 Created response if the Product is successfully added,
        /// a problematic validation response in case of validation error,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<ProductDto>> Add()
        {
            try
            {
                var formCollection = await this.Request.ReadFormAsync();
                var files = formCollection.Files;

                if (files.Count == 0)
                {
                    return this.BadRequest("No image file uploaded");
                }

                var imageFile = files[0];
                byte[] imageData = await ReadImageData(imageFile);

                var jsonDto = formCollection["dto"];
                var dto = JsonConvert.DeserializeObject<ProductDto>(jsonDto!);

                await this.service.Add(dto!, imageData);
                return StatusCode(StatusCodes.Status201Created, dto);
            }
            catch (ArgumentNullException)
            {
                return this.ValidationProblem();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Retrieves the details of a Product based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the Product to retrieve.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the Product does not exist,
        /// the details of the Product if found,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpGet("get/{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
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
        /// Updates the details of a Product based on its identifier using the provided data.
        /// </summary>
        /// <param name="id">The identifier of the Product to update.</param>
        /// <param name="dto">The new data of the Product.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the Product does not exist,
        /// a problematic validation response in case of validation error,
        /// or an HTTP 500 Internal Server Error response in case of server internal error.
        /// </returns>
        [HttpPut("update/{id}")]
        public async Task<ActionResult<ProductDto>> Update(int id)
        {
            if (id <= default(int))
            {
                return NotFound();
            }

            try
            {
                var formCollection = await this.Request.ReadFormAsync();
                var files = formCollection.Files;

                byte[] imageData = null!;
                if (files.Count > 0)
                {
                    var imageFile = files[0];
                    imageData = await ReadImageData(imageFile);
                }

                var jsonDto = formCollection["dto"];
                var dto = JsonConvert.DeserializeObject<ProductDto>(jsonDto!);

                return await this.service.Update(dto!, imageData);
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
        /// Deletes a Product based on its identifier.
        /// </summary>
        /// <param name="id">The identifier of the Product to delete.</param>
        /// <returns>
        /// Returns an HTTP 404 NotFound response if the Product does not exist,
        /// an HTTP 200 OK response if the Product is successfully deleted,
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
        public ActionResult<List<ProductDto>> GetAll()
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

        private async Task<byte[]> ReadImageData(IFormFile imageFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        [HttpGet("searchbar/{request}")]
        public ActionResult<List<ProductDto>> SearchBar(string request) 
        {
            try
            {
                return this.service.SearchBar(request);
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Internal Server Error");
            }
        }
    }
}