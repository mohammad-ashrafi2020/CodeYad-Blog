using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeYad_Blog.CoreLayer.Services.Posts;
using Microsoft.AspNetCore.Authorization;

namespace CodeYad_Blog.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IPostService _postService;

        public IndexModel(ILogger<IndexModel> logger, IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

        public void OnGet()
        {

        }

        public IActionResult OnGetPopularPost()
        {
            return Partial("_PopularPosts", _postService.GetPopularPost());
        }

    }
}
