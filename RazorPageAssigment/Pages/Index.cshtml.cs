using RazorPageAssigment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPageAssigment.Pages
{
    public class IndexModel : PageModel
    {
        private DatabaseContext _db;

        public List<Products> listPizza { get; set; }
        public List<Categories> categories { get; set; }


        public IndexModel(DatabaseContext db)
        {
            _db = db;
        }

        public void OnGet(string? productName)
        {
            if (productName == null)
            {
                listPizza = _db.Products.ToList();
                ViewData["SearchString"] = null;
            }
            else
            {
                listPizza = _db.Products.Where(product => product.ProductName.Contains(productName)).ToList();
                ViewData["SearchString"] = productName;
            }
            categories = _db.Categories.ToList();
        }
    }
}
