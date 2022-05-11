using BlogPessoal.src.dtos;
using BlogPessoal.src.repositors;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public IActionResult GetUserById([FromRoute] int idUser)
        {
            var user = _repository.GetUserById(idUser);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet]
        [Authorize(Roles ="NORMAL,ADMIN")]
        public IActionResult GetUserByName([FromQuery] string UserName)
        {
            var users = _repository.GetUserByName(UserName);

            if (users.Count < 1)
                return NoContent();

            return Ok(users);
        }

        [HttpGet("email/{emailUser}")]
        [Authorize(Roles ="NORMAL,ADMIN")]
        public IActionResult GetUserByEmail([FromRoute] string emailUser)
        {
            var user = _repository.GetUserByEmail(emailUser);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult AddUser([FromBody] AddUserDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                _services.CreateUserWithoutDuplicate(user);
                return Created($"api/Users/email/{user.Email}", user);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }


        [HttpPut]
        [Authorize(Roles = "NORMAL,ADMIN")]
        public IActionResult UpdateUser([FromBody] UpdateUserDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            user.Password = _services.EncodePassword(user.Password);

            _repository.UpdateUser(user);
            return Ok(user);
        }

        [HttpDelete("delete/{idUser}")]
        [Authorize(Roles ="ADMIN")]
        public IActionResult DeleteUser([FromRoute] int iduser)
        {
            _repository.DeleteUser(iduser);
            return NoContent();
        }
        #endregion
    }
}
