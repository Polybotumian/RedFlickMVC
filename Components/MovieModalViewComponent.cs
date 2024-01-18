using Microsoft.AspNetCore.Mvc;
using RedFlickMVC.Models;

namespace RedFlickMVC.Components
{
    public class MovieModalViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(AllViewModel model)
        {

            return View(model);
        }
    }
}