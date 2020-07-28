using GitsLibary.DataAccess.UnitOfWorks;
using GitsLibary.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GitsLibary.DataAccess.Services
{
    public class BaseService<TCoreEntity>
        where TCoreEntity : BaseEntityModel
    {
        protected readonly IUnitOfWork unitOfWork;
        public BaseService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public virtual IEnumerable<object> GetAll(out int TotalRow, int page = 0, int pageSize = 0, Expression<Func<TCoreEntity, bool>> filter = null)
        {
            return unitOfWork.GetRepository<TCoreEntity>().GetAll(out TotalRow,
                        page: page,
                        pageSize: pageSize,
                        filter: filter);
        }
        public virtual object GetById(int Id)
        {
            return unitOfWork.GetRepository<TCoreEntity>().GetById(Id);
        }
        public virtual object Get(Expression<Func<TCoreEntity, bool>> filter)
        {
            return unitOfWork.GetRepository<TCoreEntity>().Get(filter);
        }
        public virtual void Save(TCoreEntity entity)
        {
            unitOfWork.GetRepository<TCoreEntity>().Upsert(entity);
            unitOfWork.Commit();
        }
        public virtual void Delete(int Id)
        {
            unitOfWork.GetRepository<TCoreEntity>().SoftDelete(Id);
            unitOfWork.Commit();
        }
    }
}