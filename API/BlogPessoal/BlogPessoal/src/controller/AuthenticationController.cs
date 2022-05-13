using System;
using System.Threading.Tasks;
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
        public async Task<ActionResult> AuthenticationAsync([FromBody] AuthenticationDTO authentication)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var autorizacao = await _services.GetAuthorizationAsync(authentication);
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