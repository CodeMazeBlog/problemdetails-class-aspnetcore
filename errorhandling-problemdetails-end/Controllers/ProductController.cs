
using ErrorHandlingProblemDetails.Data.Models;
using ErrorHandlingProblemDetails.Services.Interfaces;
using ErrorHandlingProblemDetails.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ErrorHandlingProblemDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get() => await _productService.GetAllPrpoducts();

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            throw new ProductCustomException(Request.Path.Value);

            var product = await _productService.GetProductById(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("byName/{name}")]
        public async Task<ActionResult<Product>> GetByName(string name)
        {
            throw new Exception("There was an exception while fetching the product");

            var product = await _productService.GetProductByName(name);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task Post([FromBody] Product product) => await _productService.CreateNewProduct(product);
    }
}
