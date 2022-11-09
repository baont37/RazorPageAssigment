using RazorPageAssigment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace RazorPageAssigment.Pages
{
    public class ProductModel : PageModel
    {
        private DatabaseContext _db;

        [BindProperty (Name = "id", SupportsGet = true)]
        public int Id { get; set; }

        public Products product { get; set; }

        public ProductModel(DatabaseContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            product = _db.Products.Find(Id);
        }
    }
}
