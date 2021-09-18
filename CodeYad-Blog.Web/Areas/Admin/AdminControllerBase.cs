using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeYad_Blog.Web.Areas.Admin
{
    [Area("admin")]   
    [Authorize(Policy = "AdminPolicy")]
    public class AdminControllerBase : Controller
    {
        
    }
}