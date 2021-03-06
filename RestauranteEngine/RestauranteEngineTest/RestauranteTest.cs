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
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
            //var builder = new DbContextOptionsBuilder<BaseContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AvaliacaoCedro;integrated security=True;");
            var builder = new DbContextOptionsBuilder<BaseContext>().UseSqlServer("Server=DESKTOP-NBVUSVN\\SQLEXPRESS;Database=AvaliacaoCedro;integrated security=True;");
            db = new BaseContext(builder.Options);
            this.repo = new RestauranteRepository(db);
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

        [TestMethod]
        public void InsertTest()
        {
            var restaurantes = repo.GetAll();
            foreach (var item in restaurantes)
            {
                repo.Delete(item);
            }

            foreach (var item in this.GetRestaurantes())
            {
                repo.Save(item);
            }

            var countFinal = repo.GetAll().Count;

            Assert.AreEqual(this.GetRestaurantes().Count, countFinal);

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
                Assert.IsTrue(ex.InnerException.Message.Contains("Cannot insert the value NULL into column"));
            }
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

        [TestMethod]
        public void InsertRestaurantePost()
        {
            var restaurantesTest = new Restaurante { Nome = "Demo5" };
            var controller = new RestauranteController(db);

            var actionResult = controller.InsertRestaurante(restaurantesTest);
            var result = actionResult as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

        }

        [TestMethod]
        public void InsertRestaurantePostFail()
        {
            var restaurantesTest = new Restaurante();
            var controller = new RestauranteController(db);

            var actionResult = controller.InsertRestaurante(restaurantesTest);
            var result = actionResult as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);

        }

        [TestMethod]
        public void GetRestauranteById()
        {
            var controller = new RestauranteController(db);
            var restauranteTest = repo.Where(r => r.Nome == "Demo1").FirstOrDefault();

            var actionResult = controller.GetById(restauranteTest.Id);
            var result = actionResult as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

        }

        [TestMethod]
        public void GetRestauranteByIdFail()
        {
            var controller = new RestauranteController(db);

            var actionResult = controller.GetById(1);
            var result = actionResult as NotFoundResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);

        }

        [TestMethod]
        public void GetRestauranteByName()
        {
            var controller = new RestauranteController(db);

            var actionResult = controller.GetByName("Demo1");
            var result = actionResult as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

        }

        [TestMethod]
        public void GetRestauranteByNameFail()
        {
            var controller = new RestauranteController(db);

            var actionResult = controller.GetByName("Demo15");
            var result = actionResult as NotFoundResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);

        }

        [TestMethod]
        public void UpdateRestaurante()
        {
            var restaurantesTest = repo.Where(r => r.Nome == "Demo3").FirstOrDefault();
            restaurantesTest.Nome = "Demo6";
            var controller = new RestauranteController(db);

            var actionResult = controller.UpdateRestaurante(restaurantesTest);
            var result = actionResult as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void UpdateRestauranteFail()
        {
            var restaurantesTest = new Restaurante { Nome = "Demo5" };
            var controller = new RestauranteController(db);

            var actionResult = controller.UpdateRestaurante(restaurantesTest);
            var result = actionResult as NotFoundResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public void UpdateRestauranteFail2()
        {
            var restaurantesTest = repo.Where(r => r.Nome == "Demo6").FirstOrDefault();
            restaurantesTest.Nome = null;
            var controller = new RestauranteController(db);

            var actionResult = controller.UpdateRestaurante(restaurantesTest);
            var result = actionResult as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void DeleteRestaurante()
        {
            var restauranteTest = new Restaurante { Nome = "Demo6"};
            repo.Save(restauranteTest);
            var controller = new RestauranteController(db);

            var actionResult = controller.DeleteRestaurante(restauranteTest.Id);
            var result = actionResult as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void DeleteRestauranteFail()
        {
            var controller = new RestauranteController(db);

            var actionResult = controller.DeleteRestaurante(1);
            var result = actionResult as NotFoundResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

    }
}
