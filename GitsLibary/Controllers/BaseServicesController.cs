using GitsLibary.DataAccess.Services;
using GitsLibary.DataAccess.UnitOfWorks;
using GitsLibary.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GitsLibary.Controller
{
    public class BaseServicesController<TCoreEntity, TCoreIViewModel> : ControllerBase
            where TCoreEntity : BaseEntityModel
            where TCoreIViewModel : IBaseModel
    {
        protected ApiResponeModel globalRespone = new ApiResponeModel();
        protected int TotalRow = 0;
        protected BaseService<TCoreEntity> baseService;
        protected readonly IUnitOfWork unitOfWork;
        public BaseServicesController(IUnitOfWork _unitOfWork) { unitOfWork = _unitOfWork; }

        //GetAll
        [HttpGet("GetAll")]
        public virtual ApiResponeModel GetAll(int page = 0, int pageSize = 0)
        {
            try
            {
                int totalrow = 0;
                globalRespone.IsValid = true;
                globalRespone.Data = baseService.GetAll(out totalrow, page, pageSize);
                globalRespone.TotalRow = totalrow;
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
                globalRespone.Data = baseService.GetById(Id);
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
                baseService.Save((TCoreEntity)data);
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
                baseService.Delete(Id);
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
