using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebshopAPI.Controllers
{
    [ApiController]
    [Authorize("Admin")]
    public class AdminBaseController : Controller
    {
        protected string _CurrentUser = "System";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _CurrentUser = this.HttpContext.User.Claims.Single(a => a.Type == ClaimTypes.Email).Value;
            base.OnActionExecuting(context);
        }
    }
}
