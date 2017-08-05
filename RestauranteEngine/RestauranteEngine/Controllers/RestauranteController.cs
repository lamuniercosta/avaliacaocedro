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

        public RestauranteController(BaseContext _db)
        {
            this.db = _db;
            this.repo = new RestauranteRepository(db);
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
        public IActionResult InsertPost([FromBody]Restaurante restaurante)
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

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
