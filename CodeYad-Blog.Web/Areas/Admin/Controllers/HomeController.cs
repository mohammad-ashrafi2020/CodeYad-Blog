using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeYad_Blog.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminControllerBase
    {
        [Route("/admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
