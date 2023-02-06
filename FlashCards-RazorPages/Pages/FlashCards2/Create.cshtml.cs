using System;
using System.Collections.Generic;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Models;


namespace FlashCards_RazorPages.Pages.FlashCards2
{
    public class InputData
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        public string operatorr { get; set; }
    }
    public class CreateModel : PageModel
    {
        [BindProperty]
        public bool Addition { get; set; }
        [BindProperty]
        public static FlashCard flashCard { get; set; }
        [BindProperty]
        public FlashCard displayedflashCard { get; set; }
        [BindProperty]
        public static List<ModelQuestion> questions { get; set; }
        [BindProperty]
        public List<ModelQuestion> displayedquestions { get; set; }
        public static bool checkls { get; set; }
        [BindProperty]
        public static string Subject { get; set; }
        [BindProperty]
        public string displayedsubject { get; set; } = "Math";
        public IMediator Mediate;
        public IConfiguration configuration;
        public CreateModel(IConfiguration configuration, IMediator Mediate)
        {
            this.Mediate = Mediate;
            this.configuration = configuration;
        }
        public IActionResult OnGet()
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
            if (checkls)
            {
                checkls = false;
                displayedquestions = questions;
                displayedsubject = Subject;
            }
            else
            {
                Subject = "Math";
                displayedsubject = Subject;
                questions = new List<ModelQuestion>();
                flashCard = new FlashCard();
            }
            displayedflashCard = flashCard;

            return Page();
        }

        public IActionResult OnPost(ModelQuestion quest, bool submit, bool addition, int chapter, DateTime datee, string subject)
        {
            flashCard.ChapterNumber = chapter;
            flashCard.Date = datee;
            Subject = subject;
            displayedsubject = subject;
            ModelState.Clear();
            if (!submit)
            {
                if (quest.x == 0 && quest.y == 0 && quest.Operator == null && quest.z == 0)
                {
                    if (quest.Question == null && quest.Answer == null)
                    {
                        ModelState.AddModelError("data", "Questions data are required");

                    }
                }
            }
            if (submit)
            {
                if (chapter == 0)
                {
                    ModelState.AddModelError("chapter", "Add chapter number");
                }
                if (datee == default(DateTime))
                {
                    ModelState.AddModelError("date", "Enter the date");
                }
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (addition)
            {
                checkls = true;
                questions.Add(quest);
                displayedquestions = questions;
                return Page();
            }

            if (quest.x != 0 && quest.y != 0 && quest.Operator != null && quest.z != 0)
            {
                questions.Add(quest);
            }
            if (quest.Question != null && quest.Answer != null)
            {
                questions.Add(quest);
            }
            flashCard.SubjectName = subject;
            flashCard.ChapterNumber = chapter;
            flashCard.Date = datee;
            flashCard.Questions = questions;
            string token = HttpContext.Session.GetString("Token");

            var mes = Mediate.PostData(token, flashCard, null);
            if (mes.IsSuccessStatusCode)
            {
                return RedirectToPage("/FlashCards2/Index");
            }
            return BadRequest();

        }
        public IActionResult OnPostMath(ModelQuestion quest, string submit, int ChapterNumber, DateTime Date)
        {
            flashCard.ChapterNumber = ChapterNumber;
            flashCard.Date = Date;
            Subject = "Math";
            displayedsubject = "Math";


            if (submit == "Add")
            {
                if (quest.x == 0 || quest.y == 0 || quest.Operator == null || quest.z == 0)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("data", "Missing fields");
                    return Page();
                }
                else
                {
                    ModelState.Clear();

                    checkls = true;
                    //questions.Add(new ModelQuestion {x=quest.x,y=quest.y,z=quest.z,Operator=quest.operatorr });
                    questions.Add(quest);
                    displayedquestions = questions;
                    displayedflashCard = flashCard;
                    return Page();
                }
            }
            else
            {
                if (ChapterNumber == 0 || Date == default(DateTime))
                {
                    ModelState.Clear();
                    ModelState.AddModelError("data", "Missing fields");
                    displayedquestions = questions;
                    displayedflashCard = flashCard;
                    return Page();
                }

                if (quest.x != 0 && quest.y != 0 && quest.Operator != null && quest.z != 0)
                {
                    //questions.Add(new ModelQuestion { x = quest.x, y = quest.y, z = quest.z, Operator = quest.operatorr });
                    questions.Add(quest);
                }
                checkls = false;
                flashCard.SubjectName = "Math";
                flashCard.ChapterNumber = ChapterNumber;
                flashCard.Date = Date;
                flashCard.Questions = questions;
                string token = HttpContext.Session.GetString("Token");
                var mes = Mediate.PostData(token, flashCard, null);
                if (mes.IsSuccessStatusCode)
                {
                    return RedirectToPage("/FlashCards2/Index");
                }
                return BadRequest();
            }
        }
        public IActionResult OnPostOther([FromBody] ModelQuestion quest, string submit, int ChapterNumber, DateTime Date)
        {
            flashCard.ChapterNumber = ChapterNumber;
            flashCard.Date = Date;
            Subject = "Science";
            displayedsubject = "Science";


            if (submit == "Add")
            {
                if (quest.Question == null || quest.Answer == null)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("data", "Missing fields");
                    return Page();
                }
                else
                {
                    ModelState.Clear();

                    checkls = true;
                    questions.Add(quest);
                    displayedquestions = questions;
                    return Page();
                }
            }
            else
            {
                if (ChapterNumber == 0 || Date == default(DateTime))
                {
                    ModelState.Clear();
                    ModelState.AddModelError("data", "Missing fields");
                    displayedquestions = questions;
                    return Page();
                }
                if (quest.Question != null && quest.Answer != null)
                {
                    questions.Add(quest);
                }
                checkls = false;

                flashCard.SubjectName = "Science";
                flashCard.ChapterNumber = ChapterNumber;
                flashCard.Date = Date;
                flashCard.Questions = questions;
                string token = HttpContext.Session.GetString("Token");
                var mes = Mediate.PostData(token, flashCard, null);
                if (mes.IsSuccessStatusCode)
                {
                    return RedirectToPage("/FlashCards2/Index");
                }
                return BadRequest();
            }
        }
    }
}
