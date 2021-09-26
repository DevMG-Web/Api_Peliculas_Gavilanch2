using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPeliculas.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    { 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        public DbSet<Genero> Generos { get; set; }

        public DbSet<Pelicula> Peliculas { get; set; }
    }
}
