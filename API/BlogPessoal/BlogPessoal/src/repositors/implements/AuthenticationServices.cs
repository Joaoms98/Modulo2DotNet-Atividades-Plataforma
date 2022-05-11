using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositors.implements
{
    public class AuthenticationServices : IAuthentication
    {
        #region Atributos
        private readonly IUser _repository;
        public IConfiguration Configuration { get; }
        #endregion

        #region Construtores
        public AuthenticationServices(IUser repository, IConfiguration configuration)
        {
            _repository = repository;
            Configuration = configuration;
        }
        #endregion
        
        #region methods
        public void CreateUserWithoutDuplicate(AddUserDTO dto)
        {
            var user = _repository.GetUserByEmail(dto.Email);

            if (user != null) throw new Exception("Este email já está sendo utilizado");

            dto.Password = EncodePassword(dto.Password);

            _repository.AddUser(dto);
        }

        public string EncodePassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);
        }

        public string GenerateToken(UserModel user)
        {
            var tokenManipulador = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration["Settings:Secret"]);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Email, user.Email.ToString()),
                        new Claim(ClaimTypes.Role, user.Type.ToString())
                    }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenManipulador.CreateToken(tokenDescription);         
            return tokenManipulador.WriteToken(token);
        }
    
        public AuthorizationDTO GetAuthorization(AuthenticationDTO authentication)
        {
            var user = _repository.GetUserByEmail(authentication.Email);
            
            if (user == null) throw new Exception("Usuário não encontrado");

            if (user.Password != EncodePassword(authentication.Password)) throw new Exception("Senha incorreta");

            return new AuthorizationDTO(user.Id, user.Email, user.Type,
            GenerateToken(user));
        }
        #endregion
    }
}
