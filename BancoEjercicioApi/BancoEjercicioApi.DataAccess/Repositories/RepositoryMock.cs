using BancoEjercicioApi.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BancoEjercicioApi.DataAccess.Repositories
{
    public class RepositoryMock<T> : IRepository<T> where T : class, IBusinessObjectBase
    {
        #region Vars

        private readonly IList<T> _dbSet;

        #endregion Vars

        #region Constructor

        // Inyecta el dbContext y prepara el dbSet según la entidad
        public RepositoryMock()
        {
            _dbSet = new List<T>();
        }

        #endregion Constructor

        #region Public Methods

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteById(object id)
        {
            T? entityToDelete = _dbSet.Where(t => (int)t.IdBO == (int)id).FirstOrDefault();
            if (entityToDelete != null)
            {
                Delete(entityToDelete);
            } 
        }

        public IList<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T? GetById(object id)
        {
            return _dbSet.Where(t => (int)t.IdBO == (int)id).FirstOrDefault();
        }

        public void Add(T entity)
        {
            entity.IdBO = _dbSet.Count() + 1;
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            T? entityToUpdate = _dbSet.Where(t => (int)t.IdBO == (int)entity.IdBO).FirstOrDefault();
            if (entityToUpdate != null)
            {
                _dbSet.Remove(entityToUpdate);
                _dbSet.Add(entity);
            }
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AsQueryable().Where(predicate);
        }

        public IList<T> GetFromSQLString(FormattableString sql)
        {
            //return _dbSet.FromSqlInterpolated(sql).ToList();
            return _dbSet;
        }

        #endregion Public Methods
    }
}
