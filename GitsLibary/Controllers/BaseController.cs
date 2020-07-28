using GitsLibary.DataAccess.UnitOfWorks;
using GitsLibary.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GitsLibary.Controller
{
    public class BaseController<TCoreEntity, TCoreIViewModel> : ControllerBase
            where TCoreEntity : BaseEntityModel
            where TCoreIViewModel : IBaseModel
    {
        protected ApiResponeModel globalRespone = new ApiResponeModel();
        protected readonly IUnitOfWork unitOfWork;
        protected int TotalRow = 0;
        public BaseController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        //GetAll
        [HttpGet("GetAll")]
        public virtual ApiResponeModel GetAll(int page = 0, int pageSize = 0)
        {
            try
            {
                globalRespone.IsValid = true;
                globalRespone.Data = unitOfWork.GetRepository<TCoreEntity>().GetAll(out TotalRow,
                    page: page,
                    pageSize: pageSize);
                globalRespone.TotalRow = TotalRow;
            }
            catch (Exception e)
            {
                globalRespone.IsValid = false;
                globalRespone.Message = e.Message;
            }

            return globalRespone;
        }

        //GetById
        [HttpGet("GetById")]
        public virtual ApiResponeModel GetById(int Id)
        {
            try
            {
                globalRespone.IsValid = true;
                globalRespone.Data = unitOfWork.GetRepository<TCoreEntity>().GetById(Id);
            }
            catch (Exception e)
            {
                globalRespone.IsValid = false;
                globalRespone.Message = e.Message;
            }

            return globalRespone;
        }

        //SaveJson
        [HttpPost("SaveJson")]
        public virtual ApiResponeModel SaveJson([FromBody]TCoreIViewModel model)
        {
            return Save(model);
        }

        //SaveMultiPart
        [HttpPost("SaveMultiPart")]
        public virtual ApiResponeModel SaveMultiPart(TCoreIViewModel model)
        {
            return Save(model);
        }

        protected ApiResponeModel Save(TCoreIViewModel model)
        {
            try
            {
                var data = model.Mapping(unitOfWork);
                unitOfWork.GetRepository<TCoreEntity>().Upsert((TCoreEntity)data);
                unitOfWork.Commit();
                globalRespone.IsValid = true;
            }
            catch (Exception e)
            {
                globalRespone.IsValid = false;
                globalRespone.Message = e.Message;
            }

            return globalRespone;
        }

        [HttpPost("Delete")]
        public virtual ApiResponeModel Delete(int Id)
        {
            try
            {
                globalRespone.IsValid = true;
                unitOfWork.GetRepository<TCoreEntity>().SoftDelete(Id);
            }
            catch (Exception e)
            {
                globalRespone.IsValid = false;
                globalRespone.Message = e.Message;
            }

            return globalRespone;
        }

    }
}
