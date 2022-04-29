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

namespace BlogPessoal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration {get;}

        //String de conex�o
        public void ConfigureServices(IServiceCollection services)
        {
            //configura��o banco de dados
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json") //buscando o documento
                .Build();

            services.AddDbContext<AppBlogContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection"))); //rela��o using BlogPessoal.src.data;

            //configura��o controlador
            services.AddControllers();
        }

        // Configurando a cria��o do banco de dados na inicializa��o
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,AppBlogContext context) //chamando o banco appblogcontext
        {
            if (env.IsDevelopment())  
            {
                context.Database.EnsureCreated();  //criara o banco de dados
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
