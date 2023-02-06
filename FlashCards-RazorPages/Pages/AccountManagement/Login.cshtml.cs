using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Models;

namespace FlashCards_RazorPages.Pages.AccountManagement
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        public IMediator Mediate;

        public LoginModel(IMediator Mediate)
        {
            this.Mediate = Mediate;
        }
        [BindProperty]
        public Login login { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {

            var mes = Mediate.PostManagementData(login,null);

            if (mes.IsSuccessStatusCode)
            {
                var token = mes.Content.ReadAsStringAsync().Result;
               string[] ls= token.Split(";/");

                HttpContext.Session.SetString("Token", ls[0]);
                HttpContext.Session.SetString("IsAuthenticated", ls[1]);
                return RedirectToPage("/FlashCards/Index");
            }
            return Page();
        }
    }
}
