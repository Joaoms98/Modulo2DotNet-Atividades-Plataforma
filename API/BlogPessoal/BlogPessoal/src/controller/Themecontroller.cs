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
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddThemeAsync([FromBody] AddThemeDTO theme)
        {
            if (!ModelState.IsValid)
            return BadRequest();

            await _repository.AddThemeAsync(theme);
            return Created($"api/theme/{theme.Description}", theme);
        }

        [HttpDelete("delete/{idTheme}")]
        [Authorize(Roles ="ADMIN")]
        public async Task <ActionResult> DeleteUserAsync([FromRoute] int idtheme)
        {
            await _repository.DeleteThemeAsync(idtheme);
            return NoContent();
        }

        [HttpGet("id/{idtheme}")]
        [Authorize]
        public async Task <ActionResult> GetThemeByIdAsync([FromRoute] int idtheme)
        {
            var theme = await _repository.GetThemeByIdAsync(idtheme);

            if (theme == null)
                return NotFound();

            return Ok(theme);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetThemeByDescriptionAsync([FromQuery] string  description)
        {
            var themes = await _repository.GetThemeByDescriptionAsync(description);

            if (themes.Count < 1)
                return NotFound();

            return Ok(themes);
        }

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