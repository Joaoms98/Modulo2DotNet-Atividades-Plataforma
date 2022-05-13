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
    /// <summary>
    /// <para>Resumo: Classe responsavel por implementar IAuthentication</para>
    /// <para>Criado por: Joaoms98</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
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
        /// <summary>
        /// <para>Resumo: Método assíncrono para não duplicar um usuário</para>
        /// </summary>
        /// <param name="dto">AddUserDTO</param>
        public async Task CreateUserWithoutDuplicateAsync(AddUserDTO dto)
        {
            var user = await _repository.GetUserByEmailAsync(dto.Email);

            if (user != null) throw new Exception("Este email já está sendo utilizado");

            dto.Password = EncodePassword(dto.Password);

            await _repository.AddUserAsync(dto);
        }
        /// <summary>
        /// <para>Resumo: Método assíncrono para codificar a senha</para>
        /// </summary>
        /// <param name="password">AddUserDTO</param>
        public string EncodePassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);
        }
        /// <summary>
        /// <para>Resumo: Método assíncrono para gerar o token de usuário</para>
        /// </summary>
        /// <param name="user">AddUserDTO</param>
        /// <return>Token</return>
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
        /// <summary>
        /// <para>Resumo: Método assíncrono para autoriar o usuário</para>
        /// </summary>
        /// <param name="authentication">AddUserDTO</param>
        public async Task<AuthorizationDTO> GetAuthorizationAsync(AuthenticationDTO authentication)
        {
            var user = await _repository.GetUserByEmailAsync(authentication.Email);
            
            if (user == null) throw new Exception("Usuário não encontrado");

            if (user.Password != EncodePassword(authentication.Password)) throw new Exception("Senha incorreta");

            return new AuthorizationDTO(user.Id, user.Email, user.Type,
            GenerateToken(user));
        }
        #endregion
    }
}
