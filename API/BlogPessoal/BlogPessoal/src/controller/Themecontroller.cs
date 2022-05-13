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
    [Route("api/themes")]
    [Produces("application/json")]
    public class Themecontroller : ControllerBase
    {
        #region Atributes
        private readonly ITheme _repository;
        #endregion

        #region builders
        public Themecontroller(ITheme repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Criar novo tema
        /// </summary>
        /// <param name="theme">AddThemeDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Themes
        ///     {
        ///       	"description": "Loli
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Retorna Theme criado</response>
        /// <response code="400">Erro na requisição</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddThemeAsync([FromBody] AddThemeDTO theme)
        {
            if (!ModelState.IsValid)
            return BadRequest();

            await _repository.AddThemeAsync(theme);
            return Created($"api/theme/{theme.Description}", theme);
        }
        /// <summary>
        /// Deletar tema pelo Id
        /// </summary>
        /// <param name="idtheme">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Usuario deletado</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("delete/{idTheme}")]
        [Authorize(Roles ="ADMIN")]
        public async Task <ActionResult> DeleteUserAsync([FromRoute] int idtheme)
        {
            await _repository.DeleteThemeAsync(idtheme);
            return NoContent();
        }
        /// <summary>
        /// Pegar o tema pelo id
        /// </summary>
        /// <param name="idtheme">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o tema</response>
        /// <response code="404">Tema não existente</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ThemeModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idtheme}")]
        [Authorize]
        public async Task <ActionResult> GetThemeByIdAsync([FromRoute] int idtheme)
        {
            var theme = await _repository.GetThemeByIdAsync(idtheme);

            if (theme == null)
                return NotFound();

            return Ok(theme);
        }
        /// <summary>
        /// Pegar o tema pela descrição
        /// </summary>
        /// <param name="description">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o tema</response>
        /// <response code="404">Tema não existente</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ThemeModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetThemeByDescriptionAsync([FromQuery] string  description)
        {
            var themes = await _repository.GetThemeByDescriptionAsync(description);

            if (themes.Count < 1)
                return NotFound();

            return Ok(themes);
        }
        /// <summary>
        /// Atualizar tema
        /// </summary>
        /// <param name="theme">UpdateThemeDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /api/Themes
        ///     { 
        ///         "id": 1,
        ///       	"description": "Loli
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Retorna Theme atualizado</response>
        /// <response code="400">Erro na requisição</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize(Roles ="ADMIN")]
        public async Task<ActionResult> UpdateThemeAsync([FromBody] UpdateThemeDTO theme)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _repository.UpdateThemeAsync(theme);
            return Ok(theme);
        }
        #endregion
    }
}