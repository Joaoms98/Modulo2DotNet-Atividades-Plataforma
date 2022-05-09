using BlogPessoal.src.dtos;
using BlogPessoal.src.repositors;
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
        public IActionResult GetPostById([FromRoute] int idPost)
        {
            var post = _repository.GetPostById(idPost);

            if (post == null) return NotFound();
            return Ok(post);
        }
        [HttpGet]
        public IActionResult GetAllByPosts()
        {
            var list = _repository.GetAllByPosts();

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }

        [HttpGet("Search")]
        public IActionResult GetPostsbySearch(
           [FromQuery] string title,
           [FromQuery] string descriptiontheme,
           [FromQuery] string namecriator)
        {
            var posts = _repository.GetPostsbySearch(title, descriptiontheme, namecriator);

            if (posts.Count < 1) return NoContent();

            return Ok(posts);
        }   
        [HttpPost]
        public IActionResult AddIPost([FromBody] AddPostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.AddIPost(post);

            return Created($"api/Posts", post);
        }
        [HttpPut]
        public IActionResult UpdatePost([FromBody] UpdatePostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.UpdatePost(post);

            return Ok(post);
        }
        [HttpDelete("delete/{idPost}")]
        public IActionResult DeletePost([FromRoute] int idPost)
        {
            _repository.DeletePost(idPost);
            return NoContent();
        }
        #endregion
    }
}
    