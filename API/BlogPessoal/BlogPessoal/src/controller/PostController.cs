using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        /// <summary>
        /// Pegar o post pelo id
        /// </summary>
        /// <param name="idPost">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o tema</response>
        /// <response code="404">Tema não existente</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idPost}")]
        [Authorize]
        public async Task<ActionResult> GetPostByIdAsync([FromRoute] int idPost)
        {
            var post = await _repository.GetPostByIdAsync(idPost);

            if (post == null) return NotFound();
            return Ok(post);
        }
        /// <summary>
        /// Pegar todos os posts
        /// </summary>
        /// <param name="PostModel">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna todos os posts</response>
        /// <response code="204">nenhum post criado</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllByPostsAsync()
        {
            var list = await _repository.GetAllByPostsAsync();

            if (list.Count < 1) return NoContent();
            return Ok(list);
        }
        /// <summary>
        /// Pegar o post pelo id
        /// </summary>
        /// <param name="title"></param>
        /// <param name="descriptiontheme"></param>
        /// <param name="namecriator"></param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o Post</response>
        /// <response code="404">Post não existente</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <summary>
        /// Criar novo Post
        /// </summary>
        /// <param name="post">AddPostDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Posts
        ///     {
        ///        "Title": "My new cute loli",
        ///        "Description":"I bought my first cute loli",
        ///        "Photograph":"sdsdsdsdsdssd",
        ///        "creator":"victor@gmail.com",
        ///        "theme":"1"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna post criado</response>
        /// <response code="400">Erro na requisição</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PostModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddPostAsync([FromBody] AddPostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.AddPostAsync(post);

            return Created($"api/Posts", post);
        }
        /// <summary>
        /// Atualizar um post
        /// </summary>
        /// <param name="post">UpdatePostDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /api/Posts
        ///     {
   	    ///      "id": 1,
	    ///     "title": "My new post",
	    ///     "description":"About furrys",
	    ///     "Photograph":"sdsdsdsds",
	    ///     "EmailCreator":"victor@gmail.com",
	    ///     "DescriptionTheme":"1"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna post atualizado</response>
        /// <response code="400">Erro na requisição</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PostModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdatePostasync([FromBody] UpdatePostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.UpdatePostAsync(post);

            return Ok(post);
        }
        /// <summary>
        /// Deletar post pelo Id
        /// </summary>
        /// <param name="idPost">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Usuario deletado</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
    