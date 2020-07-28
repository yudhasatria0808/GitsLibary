using GitsLibary.DataAccess.UnitOfWorks;

namespace GitsLibary.Models
{
    public interface IBaseModel
    {
        object Mapping(IUnitOfWork _unitOfWork);
    }
}
