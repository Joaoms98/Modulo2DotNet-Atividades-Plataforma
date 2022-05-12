using BlogPessoal.src.dtos;
using BlogPessoal.src.repositors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.controller
{
    [ApiController]
    [Route("api/Posts")]
    [Produces("application/json")]
    public class PostController : ControllerBase
    {
        #region Atributes
        private readonly IPost _repository;
        #endregion Atributes

        #region Builders
        public PostController(IPost repository)
        {
            _repository = repository;
        }
        #endregion

        #region methods
        [HttpGet("id/{idPost}")]
        [Authorize]
        public async Task<ActionResult> GetPostByIdAsync([FromRoute] int idPost)
        {
            var post = await _repository.GetPostByIdAsync(idPost);

            if (post == null) return NotFound();
            return Ok(post);
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllByPostsAsync()
        {
            var list = await _repository.GetAllByPostsAsync();

            if (list.Count < 1) return NoContent();
            return Ok(list);
        }

        [HttpGet("Search")]
        [Authorize]
        public async Task<ActionResult> GetPostsbySearchAsync(
           [FromQuery] string title,
           [FromQuery] string descriptiontheme,
           [FromQuery] string namecriator)
        {
            var posts = await _repository.GetPostsbySearchAsync(title, descriptiontheme, namecriator);

            if (posts.Count < 1) return NoContent();

            return Ok(posts);
        }   
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddPostAsync([FromBody] AddPostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.AddPostAsync(post);

            return Created($"api/Posts", post);
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdatePostasync([FromBody] UpdatePostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.UpdatePostAsync(post);

            return Ok(post);
        }
        [HttpDelete("delete/{idPost}")]
        [Authorize]
        public async Task<ActionResult> DeletePostAsync([FromRoute] int idPost)
        {
            await _repository.DeletePostAsync(idPost);
            return NoContent();
        }
        #endregion
    }
}
    