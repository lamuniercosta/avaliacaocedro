using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestauranteEngine.Models;
using RestauranteEngine.Repositories;
using RestauranteEngine.Infra;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestauranteEngine.Controllers
{
    [Route("api/[controller]")]
    public class RestauranteController : Controller
    {
        private BaseContext db;
        private RestauranteRepository repo;
        private PratoRepository repoP;

        public RestauranteController(BaseContext _db)
        {
            this.db = _db;
            this.repo = new RestauranteRepository(db);
            this.repoP = new PratoRepository(db);
        }

        // GET: api/values
        [HttpGet]
        public List<Restaurante> GetRestaurantes()
        {
            return repo.GetAll();
        }

        [HttpGet]
        public async Task<List<Restaurante>> GetRestaurantesAsync()
        {
            return await Task.FromResult(repo.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var retorno = repo.GetById(id);
            if (retorno == null)
            {
                return NotFound();
            }
            return Ok(retorno);
        }

        // GET api/values/5
        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var retorno = repo.Where(x => x.Nome == name).FirstOrDefault();
            if (retorno == null)
            {
                return NotFound();
            }
            return Ok(retorno);
        }

        // POST api/values
        [HttpPost]
        public IActionResult InsertRestaurante([FromBody]Restaurante restaurante)
        {
            try
            {
                Restaurante rest = new Restaurante();
                rest.Nome = restaurante.Nome;
                repo.Save(rest);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

            return Ok("Ok");
        }

        [HttpPost]
        public IActionResult UpdateRestaurante([FromBody]Restaurante restaurante)
        {
            try
            {
                Restaurante rest = repo.GetById(restaurante.Id);
                rest.Nome = restaurante.Nome;
                repo.Update(rest);
            }
            catch (System.NullReferenceException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

            return Ok("Ok");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRestaurante(int id)
        {
            try
            {
                var rest = repo.GetById(id);
                if (rest == null)
                {
                    return NotFound();
                }
                foreach (var item in repoP.Where(p => p.RestauranteId == id).ToList())
                {
                    repoP.Delete(item);
                }
                repo.Delete(rest);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
            return Ok("Ok");
        }
    }
}
