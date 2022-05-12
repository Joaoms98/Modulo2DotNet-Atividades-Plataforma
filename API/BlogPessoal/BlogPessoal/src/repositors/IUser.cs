using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositors
{
    /// <summary>
    /// <para>Resumo: Responsavel por representar ações de CRUD de usuario</para>
    /// <para>Criado por:Joaoms98</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>

    public interface IUser
    {
        Task AddUserAsync(AddUserDTO User);
        Task UpdateUserAsync(UpdateUserDTO User);
        Task DeleteUserAsync(int id);
        Task <UserModel> GetUserByIdAsync(int id);
        Task <UserModel> GetUserByEmailAsync(string Email);
        Task<List<UserModel>> GetUserByNameAsync(string Name);
    }
}
