using RazorPageAssigment.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RazorPageAssigment.Pages
{
    public class AddProductModel : PageModel
    {
        private DatabaseContext _db;
        private IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public Products product { get; set; } = new Products();

        public AddProductModel(DatabaseContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(IFormFile file)
        {
            var supplier = _db.Suppliers.Find(product.SupplierId);
            var category = _db.Categories.Find(product.CategoryId);

            if (supplier == null || category == null)
            {
                ViewData["error"] = "Supplier Id or Category Id is invalid.";
                return Page();
            }

            var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "pizza", fileName);
            var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
            product.ProductImage = fileName;

            _db.Products.Add(product);
            _db.SaveChanges();

            return RedirectToPage("listproducts");
        }
    }
}
