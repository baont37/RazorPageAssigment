using RazorPageAssigment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace RazorPageAssigment.Pages
{
    public class ListProductsModel : PageModel
    {
        private DatabaseContext _db;

        public List<Products> listPizzas { get; set; }
        public ListProductsModel(DatabaseContext db)
        {
            _db = db;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("username") is null) return RedirectToPage("login");

            var authenticated = HttpContext.Session.GetString("type");

            if (authenticated.Equals("False")) return RedirectToPage("accessdenied");

            listPizzas = _db.Products.ToList(); 

            return Page();
        }

        public IActionResult OnGetDelete(int id)
        {
            var deletePizza = _db.Products.Find(id);

            _db.Products.Remove(deletePizza);

            _db.SaveChanges();

            return RedirectToPage();
        }
    }
}
