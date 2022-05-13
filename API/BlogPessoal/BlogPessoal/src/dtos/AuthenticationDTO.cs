using BlogPessoal.src.utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.dtos
{
    /// <summary>
    /// <para>Resumo:Classe espelho para criar um token</para>
    /// <para>Criado por: Joaoms98</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    public class AuthenticationDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public AuthenticationDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
    /// <summary>
    /// <para>Resumo:Classe espelho para autorização</para>
    /// <para>Criado por: Joaoms98</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    public class AuthorizationDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public UserType Type { get; set; }
        public string Token { get; set; }
        public AuthorizationDTO(int id, string email, UserType type, string token)
        {
            Id = id;
            Email = email;
            Type = type;
            Token = token;
        }
    }
}
