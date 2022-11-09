using RazorPageAssigment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace RazorPageAssigment.Pages
{
    public class LoginModel : PageModel
    {
        private readonly DatabaseContext _db;
        public string Msg { get; set; }
        public LoginModel(DatabaseContext db)
        {
            _db = db;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("username") != null) return RedirectToPage("index");

            return Page();
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("type");
            HttpContext.Session.Remove("id");
            ViewData["username"] = null;
            return Page();
        }

        public IActionResult OnPost(string username, string password)
        {
            var account = Check(username, password);
            if (account != null)
            {
                HttpContext.Session.SetString("username", username);
                HttpContext.Session.SetString("type", account.Type.ToString());
                HttpContext.Session.SetInt32("id", account.AccountId);
                return RedirectToPage("Index");
            }
            else
            {
                Msg = "Invalid username or password";
                return Page();
            }
        }

        private Account Check(string username, string password)
        {
            Account account = _db.Account.SingleOrDefault(x => x.UserName.Equals(username));
            if (account != null)
            {
                if (password.Equals(account.Password))
                {
                    return account;
                }
            }
            return null;
        }
    }
}
