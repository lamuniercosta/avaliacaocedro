using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestauranteEngine;
using RestauranteEngine.Infra;
using RestauranteEngine.Models;
using RestauranteEngine.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using RestauranteEngine.Controllers;
using System.Threading.Tasks;

namespace RestauranteEngineTest
{
    [TestClass]
    public class RestauranteTest
    {
        private BaseContext db;
        private RestauranteRepository repo;

        [TestInitialize]
        public void SetUp()
        {
            var builder = new DbContextOptionsBuilder<BaseContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AvaliacaoCedro;integrated security=True;");
            db = new BaseContext(builder.Options);
            this.repo = new RestauranteRepository(db);
        }
        [TestMethod]
        public void InsertTest()
        {
            Restaurante restaurante = new Restaurante();
            restaurante.Id = 0;
            restaurante.Nome = "Restaurante 1";
            var retorno = repo.Save(restaurante);

            Assert.AreEqual(retorno, 1);
            Assert.AreNotEqual(restaurante.Id, 0);

        }
        [TestMethod]
        public void InsertTestNullName()
        {
            try
            {
                Restaurante restaurante = new Restaurante();
                restaurante.Id = 0;
                var retorno = repo.Save(restaurante);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Cannot insert the value NULL into column"));
            }
        }

        [TestMethod]
        public void ListTest()
        {
            List<Restaurante> restaurantes = (List<Restaurante>)repo.GetAll();
            Assert.IsTrue(restaurantes.Count > 0);
        }

        [TestMethod]
        public void DeleteAll()
        {
            List<Restaurante> restaurantes = (List<Restaurante>)repo.GetAll();

            foreach (var item in restaurantes)
            {
                repo.Delete(item);
            }

            restaurantes = (List<Restaurante>)repo.GetAll();

            Assert.IsTrue(restaurantes.Count == 0);
        }

        [TestMethod]
        public void GetAllRestaurantes()
        {
            var restaurantesTest = (List<Restaurante>)repo.GetAll();
            var controller = new RestauranteController(db);

            List<Restaurante> retorno = controller.GetRestaurantes();
            Assert.AreEqual(restaurantesTest.Count, retorno.Count);
        }

        [TestMethod]
        public async Task GetAllRestaurantesAsync()
        {
            var restaurantesTest = (List<Restaurante>)repo.GetAll();
            var controller = new RestauranteController(db);

            List<Restaurante> retorno = await controller.GetRestaurantesAsync() as List<Restaurante>;
            Assert.AreEqual(restaurantesTest.Count, retorno.Count);
        }

        private List<Restaurante> GetRestaurantes()
        {
            var testRestaurantes = new List<Restaurante>();
            testRestaurantes.Add(new Restaurante { Nome = "Demo1" });
            testRestaurantes.Add(new Restaurante { Nome = "Demo2" });
            testRestaurantes.Add(new Restaurante { Nome = "Demo3" });
            testRestaurantes.Add(new Restaurante { Nome = "Demo4" });

            return testRestaurantes;
        }

    }
}
