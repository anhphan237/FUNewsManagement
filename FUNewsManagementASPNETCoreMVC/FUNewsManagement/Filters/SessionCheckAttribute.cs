using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FUNewsManagement.Filters
{
    public class SessionCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var AccountId = context.HttpContext.Session.GetInt32("AccountId");
            if (AccountId == null)
            {
                context.Result = new RedirectToActionResult("Login", "SystemAccounts", null);
            }
        }
    }
}
