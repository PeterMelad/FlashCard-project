using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
   public interface Icontext
    {
        List<FlashCard> GetAllData();
        void CreateCard(FlashCard flashCard);
        bool UpdateCard(FlashCard flashCard);
        bool DeleteCard(int x);
        bool DeleteSpecificQuestion(int x);
        void AddSpecificQuestion(ModelQuestion quest);
        FlashCard DetailedFlashCard(int x);
    }
}
