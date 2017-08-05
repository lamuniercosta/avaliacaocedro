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

        // GET: api/Prato/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Prato
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Prato/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
