using RazorPageAssigment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPageAssigment.Pages
{
    public class RegisterModel : PageModel
    {
        private DatabaseContext _db;

        [BindProperty (Name = "management", SupportsGet = true)]
        public string management { get; set; }
        public string Msg { get; set; }
        public RegisterModel(DatabaseContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(string username, string password, string address, string phone)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone))
            {
                Msg = "Empty username, password, address or phone";
                return Page();
            }
            else
            {
                var account = new Account
                {
                    UserName = username,
                    Password = password,
                    Type = false
                };

                var customer = new Customers
                {
                    ContactName = username,
                    Password = password,
                    Address = address,
                    Phone = phone
                };

                _db.Account.Add(account);
                _db.Customers.Add(customer);

                _db.SaveChanges();
                return RedirectToPage("index");
            }
        }
    }
}
