using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework
{
   public class dbcontext: Icontext
    {
        private readonly ApplicationDbContext db;
        public dbcontext(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void AddSpecificQuestion(ModelQuestion quest)
        {
            db.question.Add(quest);
            db.SaveChanges();
        }

        public void CreateCard(FlashCard flashCard)
        {
            db.FlashCards.Add(flashCard);
            db.SaveChanges();
        }

        public bool DeleteCard(int id)
        {
            var FC = db.FlashCards.Include(i => i.Questions).SingleOrDefault(a => a.ID == id);
            if (FC == null) return false;
            int l = FC.Questions.Count();
            while (l > 0)
            {
                DeleteSpecificQuestion(FC.Questions.ElementAt(0).id);
                l--;
            }
            db.FlashCards.Remove(FC);
            db.SaveChanges();
            return true;
        }

        public bool DeleteSpecificQuestion(int id)
        {
            var q = db.question.Find(id);
            if (q == null) return false;
            db.question.Remove(q);
            db.SaveChanges();
            return true;
        }

        public FlashCard DetailedFlashCard(int id)
        {
            return (db.FlashCards.Include(a => a.Questions).FirstOrDefault(i => i.ID == id));
        }

        public List<FlashCard> GetAllData()
        {
            return (db.FlashCards.OrderBy(a => a.SubjectName).ThenBy(b => b.ChapterNumber)
                  .Include(a => a.Questions)
                  .ToList());
        }

        public bool UpdateCard(FlashCard flashCard)
        {
            var FC = db.FlashCards.Find(flashCard.ID);
            if (FC == null)
            {
                return false;
            }
            FC.ChapterNumber = flashCard.ChapterNumber;
            FC.Date = flashCard.Date;
            FC.SubjectName = flashCard.SubjectName;
            db.Entry(FC).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }
    }
}
