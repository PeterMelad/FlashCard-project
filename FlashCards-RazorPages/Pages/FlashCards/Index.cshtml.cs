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

namespace FlashCards_RazorPages.Pages.FlashCards
{
    //[Authorize]
    public class IndexModel : PageModel
    {
        public IMediator Mediate;

        public IndexModel(IMediator Mediate)
        {
            this.Mediate = Mediate;

        }
        public List<FlashCard> flashCards { get; set; }
        public IActionResult OnGet()
        {
            var x = HttpContext.Session.GetString("IsAuthenticated");
            //if (x == null)
            //{
            //    return RedirectToPage("/AccountManagement/Login");
            //}
            string token = HttpContext.Session.GetString("Token");
            ViewData["token"] = token;

            var mes = Mediate.GetData(token);
            if (mes.IsSuccessStatusCode)
            {
                  flashCards = mes.Content.ReadAsAsync<List<FlashCard>>().Result;
            }
            return Page();
        }
    }
}
