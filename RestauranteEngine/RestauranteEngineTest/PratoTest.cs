using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestauranteEngine.Controllers;
using RestauranteEngine.Infra;
using RestauranteEngine.Models;
using RestauranteEngine.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteEngineTest
{
    [TestClass]
    public class PratoTest
    {
        private BaseContext db;
        private PratoRepository repo;
        private RestauranteRepository repoR;

        [TestInitialize]
        public void SetUp()
        {
            //var builder = new DbContextOptionsBuilder<BaseContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AvaliacaoCedro;integrated security=True;");
            var builder = new DbContextOptionsBuilder<BaseContext>().UseSqlServer("Server=DESKTOP-NBVUSVN\\SQLEXPRESS;Database=AvaliacaoCedro;integrated security=True;");
            db = new BaseContext(builder.Options);
            this.repo = new PratoRepository(db);
            this.repoR = new RestauranteRepository(db);
        }

        public List<Prato> GetPratos()
        {
            var testPratos = new List<Prato>();
            var rest1 = repoR.Where(r => r.Nome == "Demo1").FirstOrDefault();
            var rest2 = repoR.Where(r => r.Nome == "Demo2").FirstOrDefault();
            var rest4 = repoR.Where(r => r.Nome == "Demo4").FirstOrDefault();

            testPratos.Add(new Prato { RestauranteId = rest1.Id, Nome = "Prato1", Preco =  18.9m});
            testPratos.Add(new Prato { RestauranteId = rest1.Id, Nome = "Prato2", Preco =  27.5m});
            testPratos.Add(new Prato { RestauranteId = rest2.Id, Nome = "Prato3", Preco =  15m});
            testPratos.Add(new Prato { RestauranteId = rest4.Id, Nome = "Prato4", Preco =  65.8m});

            return testPratos;
        }

        [TestMethod]
        public void InsertTest()
        {
            var pratos = repo.GetAll();
            foreach (var item in pratos)
            {
                repo.Delete(item);
            }

            foreach (var item in this.GetPratos())
            {
                repo.Save(item);
            }

            var countFinal = repo.GetAll().Count;

            Assert.AreEqual(this.GetPratos().Count, countFinal);

        }

        [TestMethod]
        public void InsertTestNull()
        {
            try
            {
                Prato prato = new Prato();
                prato.Id = 0;
                var retorno = repo.Save(prato);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.InnerException.Message.Contains("Cannot insert the value NULL into column"));
            }
        }

        [TestMethod]
        public void GetAllPratos()
        {
            var pratosTest = (List<Prato>)repo.GetAll();
            var controller = new PratoController(db);

            List<Prato> retorno = controller.GetPratos();
            Assert.AreEqual(pratosTest.Count, retorno.Count);
        }

        [TestMethod]
        public async Task GetAllPratosAsync()
        {
            var PratosTest = (List<Prato>)repo.GetAll();
            var controller = new PratoController(db);

            List<Prato> retorno = await controller.GetPratosAsync() as List<Prato>;
            Assert.AreEqual(PratosTest.Count, retorno.Count);
        }

        [TestMethod]
        public void GetPratoById()
        {
            var controller = new PratoController(db);
            var pratoTest = repo.Where(r => r.Nome == "Prato1").FirstOrDefault();

            var actionResult = controller.GetById(pratoTest.Id);
            var result = actionResult as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

        }

        [TestMethod]
        public void GetPratoByIdFail()
        {
            var controller = new PratoController(db);

            var actionResult = controller.GetById(1);
            var result = actionResult as NotFoundResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);

        }

        [TestMethod]
        public void InsertPrato()
        {
            var restaurantesTest = repoR.Where(r=>r.Nome=="Demo1").FirstOrDefault();
            var pratoTest = new Prato { RestauranteId = restaurantesTest.Id, Nome = "Prato5", Preco = 50m };
            var controller = new PratoController(db);

            var actionResult = controller.InsertPrato(pratoTest);
            var result = actionResult as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

        }

        [TestMethod]
        public void InsertPratoFail()
        {
            var prato = new Prato();
            var controller = new PratoController(db);

            var actionResult = controller.InsertPrato(prato);
            var result = actionResult as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);

        }

        [TestMethod]
        public void UpdatePrato()
        {
            var pratoTest = repo.Where(r => r.Nome == "Prato3").FirstOrDefault();
            pratoTest.Nome = "Prato6";
            var controller = new PratoController(db);

            var actionResult = controller.UpdatePrato(pratoTest);
            var result = actionResult as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void UpdatePratoFail()
        {
            var pratoTest = new Prato { RestauranteId = 1, Nome = "Prato5", Preco = 50m };
            var controller = new PratoController(db);

            var actionResult = controller.UpdatePrato(pratoTest);
            var result = actionResult as NotFoundResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public void UpdatePratoFail2()
        {
            var pratoTest = repo.Where(r => r.Nome == "Prato6").FirstOrDefault();
            pratoTest.Nome = null;
            var controller = new PratoController(db);

            var actionResult = controller.UpdatePrato(pratoTest);
            var result = actionResult as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void DeletePrato()
        {
            var pratoTest = repo.Where(p=>p.Nome == "Prato6").FirstOrDefault();
            var controller = new PratoController(db);

            var actionResult = controller.DeletePrato(pratoTest.Id);
            var result = actionResult as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void DeletePratoFail()
        {
            var controller = new PratoController(db);

            var actionResult = controller.DeletePrato(1);
            var result = actionResult as NotFoundResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }
    }
}
