using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BancoEjercicioApi.DataAccess.Repositories
{
    public interface IRepository<T> where T : class
    {
        T? GetById(object id);
        IList<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void DeleteById(object id);
        void Delete(T entity);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        IList<T> GetFromSQLString(FormattableString sql);
    }
}
