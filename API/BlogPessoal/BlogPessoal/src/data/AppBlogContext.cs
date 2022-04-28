using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.src.data
{
    public class AppBlogContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }  //contexto da tabela
        public DbSet<ThemeModel> Themes { get; set; }
        public DbSet<PostModel> Posts { get; set; }

        public AppBlogContext(DbContextOptions<AppBlogContext>options) : base(options)
        {
        }
    }
}
