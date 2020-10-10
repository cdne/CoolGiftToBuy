using System.Collections.Generic;
using API.Entities;
using API.Models;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [Route("api/v{version:apiVersion}/products")]
    
    public class ProductsController : Controller
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductRepository repository, IMapper mapper, ILogger<ProductsController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        
        [HttpGet]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        public IActionResult GetProducts([FromQuery] string name, [FromQuery] string description, [FromQuery] string sortName)
        {
            var productsFromRepo = _repository.GetProducts(name,description,sortName);
            _logger.LogInformation("Show all products on a HTTP GET request.");
            return Ok(_mapper.Map<ICollection<ProductDto>>(productsFromRepo));
        }

        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        public ActionResult<ProductDto> GetProduct(int id)
        {
            var product = _repository.GetProductById(id);
            if (product is null) return NotFound();
            _logger.LogInformation("Display a product on HTTP GET request.");
            return Ok(_mapper.Map<ProductDto>(product));
        }
        
        
        [HttpPost]        
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        public ActionResult<ProductDto> AddProduct([FromBody] ProductForCreationDto product)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var productEntity = _mapper.Map<Product>(product);
            _repository.AddProduct(productEntity);
            
            var productToReturn = _mapper.Map<ProductDto>(productEntity);
            return CreatedAtAction(nameof(GetProduct), new {id = productEntity.Id, 
                version = ApiVersion.Default.ToString()}, productToReturn);
        }

        [HttpPut("{id}")]
        [MapToApiVersion("1.1")]
        public ActionResult<ProductForCreationDto> UpdateProduct(int id, [FromBody] ProductForCreationDto product)
        {
            if (!ModelState.IsValid) return BadRequest();
            var productToUpdate = _mapper.Map<Product>(product);
            _repository.UpdateProduct(id, productToUpdate);

            return Ok();
        }
        
        [HttpDelete("{id}")]
        [MapToApiVersion("1.1")]
        public IActionResult DeleteProduct(int id)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repository.DeleteProductFromDatabase(id);
            return Ok();
        }
    }
}