using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestauranteEngine.Infra;
using RestauranteEngine.Repositories;
using RestauranteEngine.Models;

namespace RestauranteEngine.Controllers
{
    [Produces("application/json")]
    [Route("api/Prato")]
    public class PratoController : Controller
    {
        private BaseContext db;
        private RestauranteRepository repoRest;
        private PratoRepository repo;

        public PratoController(BaseContext _db)
        {
            this.db = _db;
            this.repoRest = new RestauranteRepository(db);
            this.repo = new PratoRepository(db);
        }
        // GET: api/Prato
        [HttpGet]
        public List<Prato> GetPratos()
        {
            return repo.GetAll();
        }

        [HttpGet]
        public async Task<List<Prato>> GetPratosAsync()
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

        // POST: api/Prato
        [HttpPost]
        public IActionResult InsertPrato([FromBody]Prato prato)
        {
            try
            {
                Prato newPrato = new Prato();
                newPrato.RestauranteId = prato.RestauranteId;
                newPrato.Nome = prato.Nome;
                newPrato.Preco = prato.Preco;
                repo.Save(newPrato);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

            return Ok("Ok");
        }

        [HttpPost]
        public IActionResult UpdatePrato([FromBody]Prato prato)
        {
            try
            {
                Prato newPrato = repo.GetById(prato.Id);
                newPrato.RestauranteId = prato.RestauranteId;
                newPrato.Nome = prato.Nome;
                newPrato.Preco = prato.Preco;
                repo.Update(newPrato);
            }
            catch (System.NullReferenceException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

            return Ok("Ok");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult DeletePrato(int id)
        {
            try
            {
                var newPrato = repo.GetById(id);
                if (newPrato == null)
                {
                    return NotFound();
                }
                repo.Delete(newPrato);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
            return Ok("Ok");
        }
    }
}
