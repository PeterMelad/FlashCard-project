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
    public class RegisterModel : PageModel
    {
        public IMediator Mediate;

        public RegisterModel(IMediator Mediate)
        {
            this.Mediate = Mediate;
        }
        [BindProperty]
        public Register register { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            var mes = Mediate.PostManagementData(null,register);
            if (mes.IsSuccessStatusCode)
            {
                var token = mes.Content.ReadAsStringAsync().Result;

                HttpContext.Session.SetString("Token", token);
                HttpContext.Session.SetString("IsAuthenticated", "True");
                return RedirectToPage("/FlashCards/Index");

            }
            return Page();
        }
    }
}
