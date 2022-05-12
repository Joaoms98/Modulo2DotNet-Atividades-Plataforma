using BlogPessoal.src.dtos;
using BlogPessoal.src.repositors;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("id/{idUser}")]
        [Authorize(Roles ="NORMAL,ADMIN")]
        public async Task<ActionResult> GetUserByIdAsync([FromRoute] int idUser)
        {
            var user = await _repository.GetUserByIdAsync(idUser);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet]
        [Authorize(Roles ="NORMAL,ADMIN")]
        public async Task<ActionResult> GetUserByNameAsync([FromQuery] string UserName)
        {
            var users = await _repository.GetUserByNameAsync(UserName);

            if (users.Count < 1)
                return NoContent();

            return Ok(users);
        }

        [HttpGet("email/{emailUser}")]
        [Authorize(Roles ="NORMAL,ADMIN")]
        public async Task<ActionResult> GetUserByEmailAsync([FromRoute] string emailUser)
        {
            var user = await _repository.GetUserByEmailAsync(emailUser);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

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
