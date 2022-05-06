using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogPessoal.src.data;
using BlogPessoal.src.repositors;
using BlogPessoal.src.repositors.implements;

namespace BlogPessoal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration {get;}

        //String de conexão
        public void ConfigureServices(IServiceCollection services)
        {
            //configuração banco de dados
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json") //buscando o documento
                .Build();

            services.AddDbContext<AppBlogContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection"))); //relação using BlogPessoal.src.data;

            //configuração controlador
            services.AddControllers();

            services.AddScoped<IUser, UserRepository>();
            services.AddScoped<ITheme, ThemeRepository>();
            services.AddScoped<IPost, PostRepository>();

            //controllers
            services.AddCors();
            services.AddControllers();
        
        }

        // Configurando a criação do banco de dados na inicialização
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,AppBlogContext context) //chamando o banco appblogcontext
        {
            if (env.IsDevelopment())  
            {
                context.Database.EnsureCreated();  //criara o banco de dados
                app.UseDeveloperExceptionPage();
            }

            //ambiente de produção
            //Rotas
            app.UseRouting();

            app.UseCors(c => c
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
