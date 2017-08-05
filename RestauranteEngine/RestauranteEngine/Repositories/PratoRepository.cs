using Microsoft.EntityFrameworkCore;
using RestauranteEngine.Infra;
using RestauranteEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteEngine.Repositories
{
    public class PratoRepository : IUnitOfWork<Prato>
    {
        private BaseContext context;
        private DbSet<Prato> entity;
        public PratoRepository(BaseContext context)
        {
            this.context = context;
            entity = context.Set<Prato>();
        }

        public int Save(Prato model)
        {
            context.Entry(model).State = EntityState.Added;
            return this.context.SaveChanges();
        }

        public int Update(Prato model)
        {
            context.Entry(model).State = EntityState.Modified;
            return this.context.SaveChanges();
        }

        public void Delete(Prato model)
        {
            entity.Remove(model);
            context.Entry(model).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public List<Prato> GetAll()
        {
            return entity.ToList();
        }

        public Prato GetById(object id)
        {
            return entity.SingleOrDefault(s => s.Id == (int)id);
        }

        public List<Prato> Where(System.Linq.Expressions.Expression<Func<Prato, bool>> expression)
        {
            return this.entity.Where(expression).ToList();
        }

        public List<Prato> OrderBy(System.Linq.Expressions.Expression<Func<Prato, bool>> expression)
        {
            return this.entity.OrderBy(expression).ToList();
        }

    }
}
