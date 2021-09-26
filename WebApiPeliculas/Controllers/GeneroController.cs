using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPeliculas.Models;

namespace WebApiPeliculas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GeneroController : ControllerBase
    {
        public readonly ApplicationDbContext context;

        public GeneroController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Genero> Get()
        {
            return context.Generos.ToList();
        }

        [HttpGet("{id}", Name = "generoCreado")]
        public IActionResult GetById(int id)
        {
            var genero = context.Generos.Include(x => x.Peliculas).FirstOrDefault(x => x.Id == id);

            if(genero == null)
            {
                return NotFound();
            }

            return Ok(genero);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Genero genero)
        {
            if (ModelState.IsValid)
            {
                context.Generos.Add(genero);
                context.SaveChanges();

                return new CreatedAtRouteResult("generoCreado", new { id = genero.Id }, genero);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Genero genero, int id)
        {
            if(genero.Id != id)
            {
                return BadRequest();
            }

            context.Entry(genero).State = EntityState.Modified;
            context.SaveChanges();

            return Ok();
        }

        [HttpPost("{id}")]
        public IActionResult Delete(int id)
        {
            var genero = context.Generos.FirstOrDefault(x => x.Id == id);

            if(genero == null)
            {
                return NotFound();
            }

            context.Generos.Remove(genero);
            context.SaveChanges();

            return Ok(genero);
        }
    }
}
