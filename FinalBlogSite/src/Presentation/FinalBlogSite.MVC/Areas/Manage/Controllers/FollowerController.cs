using FinalBlogSite.Application.Abstractions.Services;
using FinalBlogSite.Application.ViewModels.Categorys;
using FinalBlogSite.Application.ViewModels.Follow;
using Microsoft.AspNetCore.Mvc;

namespace FinalBlogSite.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class FollowerController : Controller
    {
        private readonly IFollowService _service;

        public FollowerController(IFollowService service)
        {
            _service = service;
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateFollowVM vM)
        {
            bool result = await _service.CreateAsync(vM, ModelState);
            if (result) return RedirectToAction("index");
            return View(vM);
        }
    }
}
