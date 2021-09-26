using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPeliculas.Models
{
    public class Pelicula
    {
        public int Id { get; set; }

        [StringLength(30)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [ForeignKey("Genero")]
        public int GeneroId { get; set; }

        [JsonIgnore]
        public Genero Genero { get; set; }
    }
}
