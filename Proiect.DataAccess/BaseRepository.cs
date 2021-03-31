using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Proiect.Common;
using Proiect.DataAccess.EntityFramework;

namespace Proiect.DataAccess
{
    public class BaseRepository<TEntity> where TEntity:class,IEntity
    {
        private readonly ProjectContext Context;

        public BaseRepository(ProjectContext context)
        {
            this.Context = context;
        }

        public IQueryable<TEntity> Get()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public TEntity Insert(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            return entity;
        }

        public TEntity Update(TEntity entitty)
        {
            Context.Set<TEntity>().Update(entitty);

            return entitty;
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }
    }
}
