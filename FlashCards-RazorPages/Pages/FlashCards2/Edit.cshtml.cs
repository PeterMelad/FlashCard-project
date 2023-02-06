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

namespace FlashCards_RazorPages.Pages.FlashCards2
{
    //[Authorize(Roles = "Teacher")]
    public class EditModel : PageModel
    {
        public IMediator Mediate;
        public IConfiguration configuration;
        public EditModel(IMediator Mediate, IConfiguration configuration)
        {
            this.Mediate = Mediate;
            this.configuration = configuration;
        }
        public static List<ModelQuestion> questions { get; set; }
        [BindProperty]
        public static FlashCard displayedflashCard { get; set; }
        [BindProperty]
        public FlashCard flashCard { get; set; }
        public IActionResult OnGet(int id)
        {
            var x = HttpContext.Session.GetString("IsAuthenticated");
            //if (x == null)
            //{
            //    return RedirectToPage("/AccountManagement/Login");
            //}
            //else if (x == "User")
            //{
            //    return RedirectToPage("/AccessDenied");
            //}
            string token = HttpContext.Session.GetString("Token");
            ViewData["token"] = token;

            var mes = Mediate.GetData(token, id);
            if (mes.IsSuccessStatusCode)
            {
                flashCard = mes.Content.ReadAsAsync<FlashCard>().Result;
                displayedflashCard = flashCard;
                questions = flashCard.Questions.ToList();

            }
            return Page();

        }
        public IActionResult OnPostMath(ModelQuestion quest, string submit, int ChapterNumber, DateTime Date, int cardid)
        {
            quest.FlashCardID = cardid;
            if (submit == "Add")
            {
                if (quest.x == 0 || quest.y == 0 || quest.Operator == null || quest.z == 0)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("data", "Missing data");
                    flashCard.Questions = questions;
                    flashCard.SubjectName = "Math";
                    flashCard.Date = Date;
                    flashCard.ID = cardid;
                    flashCard.ChapterNumber = ChapterNumber;
                    return Page();
                }
                else
                {
                    string token = HttpContext.Session.GetString("Token");
                    var mes = Mediate.PostData(token, null, quest);
                    if (mes.IsSuccessStatusCode)
                    {
                        questions.Add(quest);
                        flashCard.Questions = questions;
                        flashCard.SubjectName = "Math";
                        flashCard.Date = Date;
                        flashCard.ChapterNumber = ChapterNumber;
                        flashCard.ID = cardid;
                        return Page();
                    }
                    return BadRequest();
                }
            }
            else
            {

                flashCard.ChapterNumber = ChapterNumber;
                flashCard.Date = Date;
                flashCard.SubjectName = "Math";
                flashCard.ID = cardid;

                string token = HttpContext.Session.GetString("Token");

                var mes = Mediate.PutData(token, flashCard);
                if (mes.IsSuccessStatusCode)
                {
                    return RedirectToPage("/FlashCards2/Index");
                }
                return BadRequest();
            }

        }
        public IActionResult OnPostOther(ModelQuestion quest, string submit, int ChapterNumber, DateTime Date, int cardid)
        {
            quest.FlashCardID = cardid;
            if (submit == "Add")
            {
                if (quest.Question == null || quest.Answer == null )
                {
                    ModelState.Clear();
                    ModelState.AddModelError("data", "Missing data");
                    flashCard.Questions = questions;
                    flashCard.SubjectName = "Science";
                    flashCard.Date = Date;
                    flashCard.ID = cardid;
                    flashCard.ChapterNumber = ChapterNumber;
                    return Page();
                }
                else
                {
                    string token = HttpContext.Session.GetString("Token");
                    var mes = Mediate.PostData(token, null, quest);
                    if (mes.IsSuccessStatusCode)
                    {
                        questions.Add(quest);
                        flashCard.Questions = questions;
                        flashCard.SubjectName = "Science";
                        flashCard.Date = Date;
                        flashCard.ChapterNumber = ChapterNumber;
                        flashCard.ID = cardid;
                        return Page();
                    }
                    return BadRequest();
                }
            }
            else
            {

                flashCard.ChapterNumber = ChapterNumber;
                flashCard.Date = Date;
                flashCard.SubjectName = "Science";
                flashCard.ID = cardid;

                string token = HttpContext.Session.GetString("Token");

                var mes = Mediate.PutData(token, flashCard);
                if (mes.IsSuccessStatusCode)
                {
                    return RedirectToPage("/FlashCards2/Index");
                }
                return BadRequest();
            }

        }

    }
}
