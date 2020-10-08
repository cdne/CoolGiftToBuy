using System.Collections.Generic;
using API.Models;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace API.Controllers
{
    [ApiController]
    [Route("api/products/{productId}/tags")]
    public class ProductTagsController : Controller
    {
        private readonly IProductTagRepository _productTagRepository;
        private readonly IMapper _mapper;

        public ProductTagsController(IProductTagRepository productTagRepository, IMapper mapper)
        {
            _productTagRepository = productTagRepository;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult GetProductTagsByProductId(int productId)
        {
            var allTagsForProduct = _productTagRepository.GetTagsByProductId(productId);
            
            return Ok(_mapper.Map<ICollection<TagDto>>(allTagsForProduct));
        }

        [HttpGet("{id}")]
        public IActionResult GetProductTagByTagId(int id)
        {
            var productTagByTagId = _productTagRepository.GetTagById(id);
            return Ok(_mapper.Map<TagDto>(productTagByTagId));
        }
        
    }
}