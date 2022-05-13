using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.src.data
{
    /// <summary>
    /// <para>Resumo: Classe contexto, responsavel por carregar contexto e definir DbSets</para>
    /// <para>Criado por: Joaoms98</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    public class AppBlogContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; } 
        public DbSet<ThemeModel> Themes { get; set; }
        public DbSet<PostModel> Posts { get; set; }

        public AppBlogContext(DbContextOptions<AppBlogContext>options) : base(options)
        {
        }
    }
}
