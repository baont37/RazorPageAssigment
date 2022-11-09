using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RazorPageAssigment.Pages.Components.Nav
{
    [ViewComponent(Name = "Nav")]
    public class NavigationViewComponent : ViewComponent
    {
        public string name = "123";
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.id = HttpContext.Session.GetInt32("id");
            ViewBag.type = HttpContext.Session.GetString("type");
            return View("Index");
        }
    }
}
