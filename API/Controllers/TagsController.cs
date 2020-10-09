﻿using System.Collections.Generic;
using API.Models;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/tags")]

    public class TagsController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagsController(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        
        [HttpGet]
        [MapToApiVersion("1.0")]
        public ActionResult GetTags()
        {
            var tags = _tagRepository.GetAllTags();
            return Ok(_mapper.Map<ICollection<TagDto>>(tags));
            
        }

        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        public ActionResult GetTagById(int id)
        {
            var tag = _tagRepository.GetTagById(id);
            return Ok(_mapper.Map<TagDto>(tag));
        }
    }
}