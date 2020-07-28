using Dapper;
using GitsLibary.Models;
using GitsLibary.DataAccess.Repositories;
using System;

namespace GitsLibary.DataAccess.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntityModel;
        dynamic ExecStoreProcedure(string query, DynamicParameters parameters);
        dynamic ExecRawQuery(string query, DynamicParameters parameters);
        int Commit();
    }
}
