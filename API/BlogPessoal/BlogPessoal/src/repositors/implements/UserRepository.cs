using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositors.implements
{
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

        public async Task DeleteUserAsync(int id)
        {
            _context.Users.Remove(await GetUserByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<UserModel> GetUserByEmailAsync(string Email)
        {
            return await _context.Users
                        .FirstOrDefaultAsync(u => u.Email == Email);
        }

        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public  async Task<List<UserModel>> GetUserByNameAsync(string Name)
        {
            return await _context.Users
                        .Where(u => u.Name.Contains(Name))
                        .ToListAsync();
        }

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
