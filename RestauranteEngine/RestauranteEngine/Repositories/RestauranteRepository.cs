using Microsoft.EntityFrameworkCore;
using RestauranteEngine.Infra;
using RestauranteEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteEngine.Repositories
{
    public class RestauranteRepository : IUnitOfWork<Restaurante>
    {
        private BaseContext context;
        private DbSet<Restaurante> entity;
        public RestauranteRepository(BaseContext context)
        {
            this.context = context;
            entity = context.Set<Restaurante>();
        }

        public int Save(Restaurante model)
        {
            context.Entry(model).State = EntityState.Added;
            return this.context.SaveChanges();
        }

        public int Update(Restaurante model)
        {
            return this.context.SaveChanges();
        }

        public void Delete(Restaurante model)
        {
            entity.Remove(model);
            context.SaveChanges();
        }

        public List<Restaurante> GetAll()
        {
            return entity.ToList();
        }

        public Restaurante GetById(object id)
        {
            return entity.SingleOrDefault(s => s.Id == (int)id);
        }

        public List<Restaurante> Where(System.Linq.Expressions.Expression<Func<Restaurante, bool>> expression)
        {
            return this.entity.Where(expression).ToList();
        }

        public List<Restaurante> OrderBy(System.Linq.Expressions.Expression<Func<Restaurante, bool>> expression)
        {
            return this.entity.OrderBy(expression).ToList();
        }

    }
}
