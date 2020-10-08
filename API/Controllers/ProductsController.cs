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
    [Route("api/[controller]")]
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
        public IActionResult GetProducts([FromQuery] string name, [FromQuery] string description)
        {
            var productsFromRepo = _repository.GetProducts(name,description);
            _logger.LogInformation("Show all products on a HTTP GET request.");
            return Ok(_mapper.Map<ICollection<ProductDto>>(productsFromRepo));
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetProduct(int id)
        {
            var product = _repository.GetProductById(id);
            if (product is null) return NotFound();
            _logger.LogInformation("Display a product on HTTP GET request.");
            return Ok(_mapper.Map<ProductDto>(product));
        }
        
        
        [HttpPost]
        public ActionResult<ProductDto> AddProduct([FromBody] ProductForCreationDto product)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var productEntity = _mapper.Map<Product>(product);
            _repository.AddProduct(productEntity);
            
            var productToReturn = _mapper.Map<ProductDto>(productEntity);
            return CreatedAtAction("GetProduct", new {id = productEntity.Id}, productToReturn);
        }
    }
}