using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiPeliculas.Models;

namespace WebApiPeliculas
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                //options.UseInMemoryDatabase("MoviesDB")
                options.UseSqlServer(
                    Configuration.GetConnectionString("defaultConnection"))
            );

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = "yourdomain.com",
                  ValidAudience = "yourdomain.com",
                  IssuerSigningKey = new SymmetricSecurityKey(
                 Encoding.UTF8.GetBytes(Configuration["Llave_super_secreta"])),
                  ClockSkew = TimeSpan.Zero
              });

            services.AddMvc()
                .AddJsonOptions(ConfigureJson)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        private void ConfigureJson(MvcJsonOptions obj)
        {
            obj.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            if (!context.Generos.Any())
            {
                context.Generos.AddRange(
                    new Genero()
                    {
                        Nombre = "Terror",
                        Descripcion = "Dan mucho miedo",
                        Peliculas = new List<Pelicula>()
                        {
                            new Pelicula()
                            {
                                Nombre = "Freddy Crugger", Descripcion = "Es una pelicula que da miedo y risa"
                            },
                            new Pelicula()
                            {
                                Nombre = "Anabel", Descripcion = "Es una muñeca que da miedo"
                            }
                        }
                    },
                    new Genero() { Nombre = "Comedia", Descripcion = "Dan mucha risa" },
                    new Genero() { Nombre = "Ciencia Ficcion", Descripcion = "Avengers Assambly" }
                );

                context.SaveChanges();
            }
        }
    }
}
