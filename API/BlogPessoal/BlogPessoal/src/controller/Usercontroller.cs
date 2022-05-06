using BlogPessoal.src.dtos;
using BlogPessoal.src.repositors;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.src.controller
{
    [ApiController]
    [Route("api/Users")]
    [Produces("application/json")]
    public class Usercontroller : ControllerBase
    {
        #region Atributes
        private readonly IUser _repository;
        #endregion

        #region Construtores
        public Usercontroller(IUser repository)
        {
            _repository = repository;
        }
        #endregion

        #region Méthods
        [HttpGet("id/{idUser}")]
        public IActionResult GetUserById([FromRoute] int idUser)
        {
            var user = _repository.GetUserById(idUser);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetUserByName([FromQuery] string UserName)
        {
            var users = _repository.GetUserByName(UserName);

            if (users.Count < 1)
                return NoContent();

            return Ok(users);
        }

        [HttpGet("email/{emailUser}")]
        public IActionResult GetUserByEmail([FromRoute] string emailUser)
        {
            var user = _repository.GetUserByEmail(emailUser);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] AddUserDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _repository.AddUser(user);
            return Created($"api/Users/{user.Email}", user);
        }
                
        [HttpPut]
        public IActionResult UpdateUser([FromBody] UpdateUserDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _repository.UpdateUser(user);
            return Ok(user);
        }

        [HttpDelete("delete/{idUser}")]
        public IActionResult DeleteUser([FromRoute] int iduser)
        {
            _repository.DeleteUser(iduser);
            return NoContent();
        }
        #endregion
    }
}
