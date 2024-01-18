using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RedFlickMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdminRole")]
    //[ValidateAntiForgeryToken]
    public class PanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
