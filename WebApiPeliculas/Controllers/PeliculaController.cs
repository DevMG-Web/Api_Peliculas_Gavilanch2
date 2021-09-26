using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPeliculas.Models;

namespace WebApiPeliculas.Controllers
{
    [Route("api/Genero/{GeneroId}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PeliculaController : ControllerBase
    {
        public ApplicationDbContext _context { get; set; }

        public PeliculaController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IEnumerable<Pelicula> GetAll(int GeneroId)
        {
            return this._context.Peliculas.Where(x => x.GeneroId == GeneroId).ToList();
        }

        [HttpGet("{id}", Name = "peliculaById")]
        public IActionResult GetById(int id)
        {
            var pelicula = this._context.Peliculas.FirstOrDefault(x => x.Id == id);

            if(pelicula == null)
            {
                return NotFound();
            }

            return Ok(pelicula);
        }

        [HttpPost]

        public IActionResult Create([FromBody] Pelicula pelicula, int GeneroId)
        {
            pelicula.GeneroId = GeneroId;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this._context.Peliculas.Add(pelicula);
            this._context.SaveChanges();

            return new CreatedAtRouteResult("peliculaById", new { id = pelicula.Id }, pelicula);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Pelicula pelicula, int id)
        {
            if(pelicula.Id != id)
            {
                return BadRequest();
            }

            this._context.Entry(pelicula).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this._context.SaveChanges();

            return Ok(pelicula);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var pelicula = this._context.Peliculas.FirstOrDefault(x => x.Id == id);

            if(pelicula == null)
            {
                return NotFound();
            }

            this._context.Peliculas.Remove(pelicula);
            this._context.SaveChanges();

            return Ok(pelicula);
        }
    }
}
