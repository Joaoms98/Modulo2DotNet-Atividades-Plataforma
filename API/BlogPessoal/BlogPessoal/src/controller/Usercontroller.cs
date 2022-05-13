using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositors;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogPessoal.src.controller
{
    [ApiController]
    [Route("api/Users")]
    [Produces("application/json")]
    public class Usercontroller : ControllerBase
    {
        #region Atributes
        private readonly IUser _repository;
        private readonly IAuthentication _services;
        #endregion

        #region Construtores
        public Usercontroller(IUser repository, IAuthentication services)
        {
            _repository = repository;
            _services = services;
        }
        #endregion

        #region Méthods
        /// <summary>
        /// Pegar usuario pelo Id
        /// </summary>
        /// <param name="idUser">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o usuario</response>
        /// <response code="404">Usuario não existente</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idUser}")]
        [Authorize(Roles ="NORMAL,ADMIN")]
        public async Task<ActionResult> GetUserByIdAsync([FromRoute] int idUser)
        {
            var user = await _repository.GetUserByIdAsync(idUser);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
        /// <summary>
        /// Pegar usuario pelo nome
        /// </summary>
        /// <param name="UserName">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o usuario</response>
        /// <response code="404">Usuario não existente</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Authorize(Roles ="NORMAL,ADMIN")]
        public async Task<ActionResult> GetUserByNameAsync([FromQuery] string UserName)
        {
            var users = await _repository.GetUserByNameAsync(UserName);

            if (users.Count < 1)
                return NoContent();

            return Ok(users);
        }
        /// <summary>
        /// Pegar usuario pelo email
        /// </summary>
        /// <param name="emailUser">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o usuario</response>
        /// <response code="404">Usuario não existente</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("email/{emailUser}")]
        [Authorize(Roles ="NORMAL,ADMIN")]
        public async Task<ActionResult> GetUserByEmailAsync([FromRoute] string emailUser)
        {
            var user = await _repository.GetUserByEmailAsync(emailUser);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
        /// <summary>
        /// Criar novo Usuario
        /// </summary>
        /// <param name="user">AddUserDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Users
        ///     {
        ///        "nome": "Joao",
        ///        "email": "Joao@domain.com",
        ///        "senha": "134652",
        ///        "foto": "URLFOTO",
        ///        "tipo": "NORMAL"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna usuario criado</response>
        /// <response code="400">Erro na requisição</response>
        /// <response code="401">E-mail ja cadastrado</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AddUserAsync([FromBody] AddUserDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
               await _services.CreateUserWithoutDuplicateAsync(user);
                return Created($"api/Users/email/{user.Email}", user);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// Atualizar Usuario
        /// </summary>
        /// <param name="user">UpdateUserDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /api/Users
        ///     {
        ///        "id": 1,    
        ///        "nome": "Joao2",
        ///        "senha": "134652",
        ///        "foto": "URLFOTO"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Retorna usuario atualizado</response>
        /// <response code="400">Erro na requisição</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize(Roles = "NORMAL,ADMIN")]
        public async Task<ActionResult> UpdateUserAsync([FromBody] UpdateUserDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            user.Password = _services.EncodePassword(user.Password);

            await _repository.UpdateUserAsync(user);
            return Ok(user);
        }
        /// <summary>
        /// Deletar usuario pelo Id
        /// </summary>
        /// <param name="iduser">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Usuario deletado</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("delete/{idUser}")]
        [Authorize(Roles ="ADMIN")]
        public async Task<ActionResult> DeleteUserAsync([FromRoute] int iduser)
        {
            await _repository.DeleteUserAsync(iduser);
            return NoContent();
        }
        #endregion
    }
}
