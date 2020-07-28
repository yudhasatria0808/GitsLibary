using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace GitsLibary.Validations
{
    public class ValidatorActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.ModelState.IsValid)
            {
                var errors = filterContext.ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage)).ToList();

                var result = new 
                {
                    IsValid = false,
                    ErrorMsg = string.Join("||", errors)
                };

                filterContext.Result = new OkObjectResult(result);
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
    }
}
