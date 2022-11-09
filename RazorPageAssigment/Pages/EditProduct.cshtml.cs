using RazorPageAssigment.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RazorPageAssigment.Pages
{
    public class EditProductModel : PageModel
    {
        private DatabaseContext _db;
        private IWebHostEnvironment _webHostEnvironment;

        [BindProperty(Name = "id", SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public Products product { get; set; }

        public EditProductModel(DatabaseContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public void OnGet()
        {
            product = _db.Products.Find(Id);
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

            if (file != null)
            {
                var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "pizza", fileName);
                var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
                product.ProductImage = fileName;
            }
            else
            {
                var previousProduct = await _db.Products.AsNoTracking().SingleOrDefaultAsync(x => x.ProductId.Equals(product.ProductId));
                product.ProductImage = previousProduct.ProductImage;
            }

            _db.Entry(product).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToPage("listproducts");
        }
    }
}
