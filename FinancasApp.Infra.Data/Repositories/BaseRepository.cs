using FinancasApp.Domain.Interfaces.Repositories;
using FinancasApp.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Infra.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _dataContext;

        public BaseRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Add(TEntity entity)
        {

            _dataContext.Add(entity);
            _dataContext.SaveChanges();

        }

        public void Update(TEntity entity)
        {

            _dataContext.Update(entity);
            _dataContext.SaveChanges();


        }

        public void Delete(TEntity entity)
        {

            _dataContext.Remove(entity);
            _dataContext.SaveChanges();


        }

        public IList<TEntity> GetAll()
        {

            return _dataContext.Set<TEntity>().ToList();

        }

        public TEntity GetById(Guid id)
        {

            return _dataContext.Set<TEntity>().Find(id);

        }
    }
}
