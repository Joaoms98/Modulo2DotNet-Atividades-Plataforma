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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BlogPessoal.src.services;

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

            services.AddScoped<IUser, UserRepository>();
            services.AddScoped<ITheme, ThemeRepository>();
            services.AddScoped<IPost, PostRepository>();

            //controllers
            services.AddCors();
            services.AddControllers();

            // Configura��o de Servi�os
            services.AddScoped<IAuthentication, AuthenticationServices>();

            // Configura��o do Token Autentica��o JWTBearer
            var key = Encoding.ASCII.GetBytes(Configuration["Settings:Secret"]);
            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme =
                JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(b =>
            {
                b.RequireHttpsMetadata = false;
                b.SaveToken = true;
                b.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            }
            );
        }

        // Configurando a cria��o do banco de dados na inicializa��o
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,AppBlogContext context) //chamando o banco appblogcontext
        {
            if (env.IsDevelopment())  
            {
                context.Database.EnsureCreated();  //criara o banco de dados
                app.UseDeveloperExceptionPage();
            }

            //ambiente de produ��o
            //Rotas
            app.UseRouting();

            app.UseCors(c => c
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            // Autentica��o e Autoriza��o
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
