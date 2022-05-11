using BlogPessoal.src.utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{
    /// <summary>
    /// <para>Resumo:Classe espelho para criar um novo usuário</para>
    /// <para>Criado por: Joaoms98</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class AddUserDTO
    {
        [Required, StringLength(30)]
        public string Name { get; set; }

        [Required, StringLength(30)]
        public string Email { get; set; }

        [Required, StringLength(30)]
        public string Password { get; set; }

        public string Photograph { get; set; }

        [Required]
        public UserType Type { get; set; }

        public AddUserDTO(string name, string email,string password,string photograph,UserType type )
        {
            Name = name;
            Email = email;
            Password = password;
            Photograph = photograph;
            Type = type;
        }
    }
    /// <summary>
    /// <para>Resumo:Classe espelho para alterar um novo usuário</para>
    /// <para>Criado por: Joaoms98</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class UpdateUserDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Name { get; set; }

        [Required, StringLength(30)]
        public string Password { get; set; }

        public string Photograph { get; set; }

        public UpdateUserDTO(string name, string password, string photograph,string type)
        {
            Name = name;
            Password = password;
            Photograph = photograph;
        }
    }
}
