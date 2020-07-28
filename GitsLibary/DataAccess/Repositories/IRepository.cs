using GitsLibary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GitsLibary.DataAccess.Repositories
{
    public interface IRepository<T> where T : BaseEntityModel
    {
        T GetById(int id);
        T Get(Expression<Func<T, bool>> filter);
        IEnumerable<T> GetAll(out int totalRecords, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", int page = 0, int pageSize = 0);
        object ExecStoreProcedure(string query, params object[] parameters);
        void Upsert(T entity);
        void SoftDelete(int id);
        void Delete(int id);
    }
}
