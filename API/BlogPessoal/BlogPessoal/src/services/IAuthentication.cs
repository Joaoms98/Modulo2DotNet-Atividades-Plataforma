using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.services
{
    public interface IAuthentication
    {
        string EncodePassword(string password);
        void CreateUserWithoutDuplicate(AddUserDTO user);
        string GenerateToken(UserModel user);
        AuthorizationDTO GetAuthorization(AuthenticationDTO authentication);
    }
}
