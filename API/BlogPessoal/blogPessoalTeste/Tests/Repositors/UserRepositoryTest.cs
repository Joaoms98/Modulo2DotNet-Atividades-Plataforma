using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositors;
using BlogPessoal.src.repositors.implements;
using BlogPessoal.src.utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPessoalTeste.Tests.Repositors
{
    [TestClass]
    public class UserRepositoryTest
    {
        private AppBlogContext _context;
        private IUser _repository;

        [TestInitialize]
        public void InitialSettings()
        {
            var opt = new DbContextOptionsBuilder<AppBlogContext>()
            .UseInMemoryDatabase(databaseName: "db_blogpessoal")
            .Options;
            _context = new AppBlogContext(opt);
            _repository = new UserRepository(_context);
        }

        [TestMethod]
        public void CreatefourUser()
        {
            _repository.AddUserAsync(
            new AddUserDTO(
            "Joao",
            "Joaoo@email.com",
            "134652",
            "URLFOTO",UserType.ADMIN));
            _repository.AddUserAsync(
            new AddUserDTO(
            "Manuella",
            "manu@email.com",
            "134652",
            "URLFOTO",UserType.ADMIN));
            _repository.AddUserAsync(
            new AddUserDTO(
            "Michelle",
            "Mi@email.com",
            "134652",
            "URLFOTO", UserType.ADMIN));
            _repository.AddUserAsync(
            new AddUserDTO(
            "Pastor valdomiro santiago",
            "Prvaldomiro@email.com",
            "134652",
            "URLFOTO", UserType.ADMIN));
            //WHEN - Quando pesquiso lista total
            //THEN - Então recebo 5 usuarios
            Assert.AreEqual(5, _context.Users.Count());
        }
        [TestMethod]
        public void PegarUsuarioPeloEmailRetornaNaoNulo()
        {
            //GIVEN - Dado que registro um usuario no banco
            _repository.AddUserAsync(
            new AddUserDTO(
            "Variola da silva",
            "Variola@email.com",
            "134652",
            "URLFOTO",UserType.ADMIN));
            //WHEN - Quando pesquiso pelo email deste usuario
            var user = _repository.GetUserByEmailAsync("Variola@email.com");
            //THEN - Então obtenho um usuario
            Assert.IsNotNull(user);
        }
    }
}
