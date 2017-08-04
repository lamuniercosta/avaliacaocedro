﻿using System;
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
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
