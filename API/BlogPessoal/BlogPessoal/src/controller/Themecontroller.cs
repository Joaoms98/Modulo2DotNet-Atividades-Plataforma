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
        public IActionResult AddTheme([FromBody] AddThemeDTO theme)
        {
            if (!ModelState.IsValid)
            return BadRequest();

            _repository.AddTheme(theme);
            return Created($"api/theme/{theme.Description}", theme);
        }

        [HttpDelete("delete/{idTheme}")]
        public IActionResult DeleteUser([FromRoute] int idtheme)
        {
            _repository.DeleteTheme(idtheme);
            return NoContent();
        }

        [HttpGet("id/{idtheme}")]
        public IActionResult GetThemeById([FromRoute] int idtheme)
        {
            var theme = _repository.GetThemeById(idtheme);

            if (theme == null)
                return NotFound();

            return Ok(theme);
        }

        [HttpGet]
        public IActionResult GetThemeByDescription([FromQuery] string  description)
        {
            var themes = _repository.GetThemeByDescription(description);

            if (themes.Count < 1)
                return NotFound();

            return Ok(themes);
        }

        [HttpPut]
        public IActionResult UpdateTheme([FromBody] UpdateThemeDTO theme)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _repository.UpdateTheme(theme);
            return Ok(theme);
        }
        #endregion
    }
}