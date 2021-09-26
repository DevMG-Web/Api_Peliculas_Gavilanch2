using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPeliculas.Models
{
    public class Genero
    {
        public Genero()
        {
            Peliculas = new List<Pelicula>();
        }
        public int Id { get; set; }

        [StringLength(30)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public List<Pelicula> Peliculas { get; set; }
    }
}
