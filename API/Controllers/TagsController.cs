using System.Collections.Generic;
using API.Entities;
using API.Models;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
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
        [MapToApiVersion("1.1")]
        public ActionResult GetTags()
        {
            var tags = _tagRepository.GetAllTags();
            return Ok(_mapper.Map<ICollection<TagDto>>(tags));
            
        }

        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        public ActionResult GetTagById(int id)
        {
            var tag = _tagRepository.GetTagById(id);
            return Ok(_mapper.Map<TagDto>(tag));
        }

        [HttpPost]
        [MapToApiVersion("1.1")]
        public IActionResult AddTag([FromBody] TagForCreationDto tagForCreationDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var tag = _mapper.Map<Tag>(tagForCreationDto);
            _tagRepository.Add(tag);

            var tagToReturn = _mapper.Map<TagDto>(tag);

            return CreatedAtAction(nameof(GetTagById),
                new {id = tagToReturn.Id, version = ApiVersion.Default.ToString()},
                tagToReturn);
        }

        [HttpPut("{id}")]
        [MapToApiVersion("1.1")]
        public IActionResult UpdateTag(int id, [FromBody] TagDto tagDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var tag = _mapper.Map<Tag>(tagDto);
            _tagRepository.Update(id, tag);

            var tagToReturn = _mapper.Map<TagDto>(tag);
            tagToReturn.Id = id;

            return AcceptedAtAction(nameof(GetTagById), new {id = tagToReturn.Id, version = ApiVersion.Default.ToString()},
                tagToReturn);
        }

        [HttpPatch("{id}")]
        [MapToApiVersion("1.1")]
        public IActionResult PartiallyUpdateTag(int id,[FromBody] JsonPatchDocument<TagDto> patchTag)
        {
            if (!ModelState.IsValid) return BadRequest();

            var tag = _tagRepository.GetTagById(id);
            var tagDto = _mapper.Map<TagDto>(tag);
            
            patchTag.ApplyTo(tagDto);

            _mapper.Map(tagDto, tag);
            _tagRepository.PartiallyUpdate(tag);
            
            return AcceptedAtAction(nameof(GetTagById), new {id = tagDto.Id, 
                    version = ApiVersion.Default.ToString()}, 
                tagDto);
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("1.1")]
        public IActionResult DeleteTag(int id)
        {
            var tag = _tagRepository.GetTagById(id);
            _tagRepository.Delete(tag);
            return Ok($"Tag with id {id} was deleted.");
        }
    }
}