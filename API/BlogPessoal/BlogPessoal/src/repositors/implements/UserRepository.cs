using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Linq;

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
        public void AddUser(AddUserDTO User)
        {
            _context.Users.Add(new UserModel
            {
                Name = User.Name,
                Email = User.Email,
                Password = User.Password,
                Photograph = User.Photograph,
                Type = User.Type
            });
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            _context.Users.Remove(GetUserById(id));
            _context.SaveChanges();
        }

        public UserModel GetUserByEmail(string Email)
        {
            return _context.Users
                        .FirstOrDefault(u => u.Email == Email);
        }

        public UserModel GetUserById(int id)
        {
            return _context.Users
                .FirstOrDefault(u => u.Id == id);
        }

        public List<UserModel> GetUserByName(string Name)
        {
            return _context.Users
                        .Where(u => u.Name.Contains(Name))
                        .ToList();
        }

        public void UpdateUser(UpdateUserDTO User)
        {
            var userModel = GetUserById(User.Id);
            userModel.Name = User.Name;
            userModel.Password = User.Password;
            userModel.Photograph = User.Photograph;
            _context.Users.Update(userModel);
            _context.SaveChanges();
        }
        #endregion métods
    }
}
