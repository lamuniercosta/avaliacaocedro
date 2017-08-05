using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestauranteEngine.Controllers;
using RestauranteEngine.Infra;
using RestauranteEngine.Models;
using RestauranteEngine.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteEngineTest
{
    [TestClass]
    public class PratoTest
    {
        private BaseContext db;
        private PratoRepository repo;

        [TestInitialize]
        public void SetUp()
        {
            //var builder = new DbContextOptionsBuilder<BaseContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AvaliacaoCedro;integrated security=True;");
            var builder = new DbContextOptionsBuilder<BaseContext>().UseSqlServer("Server=DESKTOP-NBVUSVN\\SQLEXPRESS;Database=AvaliacaoCedro;integrated security=True;");
            db = new BaseContext(builder.Options);
            this.repo = new PratoRepository(db);
        }

        public List<Prato> GetPratos()
        {
            var testPratos = new List<Prato>();
            testPratos.Add(new Prato { RestauranteId = 1, Nome = "Prato1", Preco =  18.9m});
            testPratos.Add(new Prato { RestauranteId = 1, Nome = "Prato2", Preco =  27.5m});
            testPratos.Add(new Prato { RestauranteId = 2, Nome = "Prato3", Preco =  15m});
            testPratos.Add(new Prato { RestauranteId = 4, Nome = "Prato4", Preco =  65.8m});

            return testPratos;
        }

        [TestMethod]
        public void InsertTest()
        {
            var pratos = repo.GetAll();
            var countInicio = pratos.Count;
            foreach (var item in pratos)
            {
                repo.Delete(item);
            }

            foreach (var item in this.GetPratos())
            {
                repo.Save(item);
            }

            var countFinal = repo.GetAll().Count;

            Assert.AreEqual(countInicio, countFinal);
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
    }
}
