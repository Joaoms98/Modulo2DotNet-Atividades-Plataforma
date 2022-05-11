using System;
using BlogPessoal.src.dtos;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace BlogPessoal.src.controladores
{
    [ApiController]
    [Route("api/Authentication")]
    [Produces("application/json")]
    public class AuthenticationController : ControllerBase
    {
        #region Atributos
        private readonly IAuthentication _services;
        #endregion
        #region Construtores
        public AuthenticationController(IAuthentication services)
        {
            _services = services;
        }
        #endregion

        #region Métodos
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] AuthenticationDTO authentication)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var autorizacao = _services.GetAuthorization(authentication);
                return Ok(autorizacao);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        #endregion
    }
}