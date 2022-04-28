using BlogPessoal.src.data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using BlogPessoal.src.models;
using System.Linq;

namespace blogPessoalTeste.Tests.data
{
    [TestClass]
    public class AppBlogContextTest
    {
        private AppBlogContext context;

        [TestInitialize]
        public void setup()
        {
            var options = new DbContextOptionsBuilder<AppBlogContext>()
            .UseInMemoryDatabase(databaseName:"db_blogpessoal")
            .Options;
            
            context = new AppBlogContext(options);
        }

        [TestMethod]
        public void InsertNewUserInDataBaseReturnUser()
        {
            UserModel user = new UserModel();
            user.Name = "João";
            user.Email = "Joao@gmail.com";
            user.Password = "vermelho18";
            user.Photograph = "fdsjhjfjdshfjsdhfjfoto";

            context.Users.Add(user); //add user
            context.SaveChanges(); // Commit create and save

            Assert.IsNotNull(context.Users.FirstOrDefault(u => u.Email == "Joao@gmail.com")); //consult
        }

    }
}
