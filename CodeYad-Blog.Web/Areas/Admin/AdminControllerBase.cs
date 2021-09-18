using CodeYad_Blog.CoreLayer.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CodeYad_Blog.Web.Areas.Admin
{
    [Area("admin")]   
    [Authorize(Policy = "AdminPolicy")]
    public class AdminControllerBase : Controller
    {
        protected IActionResult RedirectAndShowAlert(OperationResult result, IActionResult redirectPath)
        {
            var model = JsonConvert.SerializeObject(result);
            HttpContext.Response.Cookies.Append("SystemAlert", model);
            if (result.Status != OperationResultStatus.Success)
                return View();
            return redirectPath;
        }
        protected void SuccessAlert()
        {
            var model = JsonConvert.SerializeObject(OperationResult.Success());
            HttpContext.Response.Cookies.Append("SystemAlert", model);
        }
        protected void SuccessAlert(string message)
        {
            var model = JsonConvert.SerializeObject(OperationResult.Success(message));
            HttpContext.Response.Cookies.Append("SystemAlert", model);
        }
        protected void ErrorAlert()
        {
            var model = JsonConvert.SerializeObject(OperationResult.Error());
            HttpContext.Response.Cookies.Append("SystemAlert", model);
        }
        protected void ErrorAlert(string message)
        {
            var model = JsonConvert.SerializeObject(OperationResult.Error(message));
            HttpContext.Response.Cookies.Append("SystemAlert", model);
        }
    }
}