using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositors.implements
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por implementar IUsuario</para>
    /// <para>Criado por: Joaoms98</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    public class UserRepository : IUser
    {
        #region atributes
        private readonly AppBlogContext _context;
        #endregion atributes

        #region Constructors
        public UserRepository (AppBlogContext context)
        {
           _context = context;
        }
        #endregion Constructors

        #region métods
        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar um novo usuario</para>
        /// </summary>
        /// <param name="User">AddUserDTO</param>
        public async Task AddUserAsync(AddUserDTO User)
        {
            await _context.Users.AddAsync(new UserModel
            {
                Name = User.Name,
                Email = User.Email,
                Password = User.Password,
                Photograph = User.Photograph,
                Type = User.Type
            });
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar um usuario</para>
        /// </summary>
        /// <param name="id">Id do usuario</param>
        public async Task DeleteUserAsync(int id)
        {
            _context.Users.Remove(await GetUserByIdAsync(id));
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um usuario pelo email</para>
        /// </summary>
        /// <param name="Email">Email do usuario</param>
        /// <return>UserModel</return>
        public async Task<UserModel> GetUserByEmailAsync(string Email)
        {
            return await _context.Users
                        .FirstOrDefaultAsync(u => u.Email == Email);
        }
        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um usuario pelo Id</para>
        /// </summary>
        /// <param name="id">Id do usuario</param>
        /// <return>UserModel</return>
        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar usuarios pelo nome</para>
        /// </summary>
        /// <param name="Name">Nome do usuario</param>
        /// <return>Lista UserModel</return>
        public async Task<List<UserModel>> GetUserByNameAsync(string Name)
        {
            return await _context.Users
                        .Where(u => u.Name.Contains(Name))
                        .ToListAsync();
        }
        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar um usuario</para>
        /// </summary>
        /// <param name="User">UpdateUserDTO</param>
        public async Task UpdateUserAsync(UpdateUserDTO User)
        {
            var userModel = await GetUserByIdAsync(User.Id);
            userModel.Name = User.Name;
            userModel.Password = User.Password;
            userModel.Photograph = User.Photograph;
            _context.Users.Update(userModel);
            _context.SaveChanges();
        }
        #endregion métods
    }
}
