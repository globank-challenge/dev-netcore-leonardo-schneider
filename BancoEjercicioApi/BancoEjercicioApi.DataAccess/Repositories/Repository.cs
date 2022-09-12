using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BancoEjercicioApi.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Vars

        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        #endregion Vars

        #region Constructor

        // Inyecta el dbContext y prepara el dbSet según la entidad
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        #endregion Constructor

        #region Public Methods

        public void Delete(T entity)
        {
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public void DeleteById(object id)
        {
            T? entityToDelete = _dbSet.Find(id);
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
            return _dbSet.Find(id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Entry<T>(entity).State = EntityState.Modified;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        { 
            return _dbSet.AsNoTracking().Where(predicate);
        }

        public IList<T> GetFromSQLString(FormattableString sql)
        {
            return _dbSet.FromSqlInterpolated(sql).ToList();
        }

        #endregion Public Methods
    }
}
