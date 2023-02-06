using Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Interfaces
{
   public interface IMediator
    {
       // HttpResponseMessage GetData(int id=0);
        HttpResponseMessage GetData(string token,int id=0);
        HttpResponseMessage PostData(string token, FlashCard fs,ModelQuestion q);
        HttpResponseMessage PostManagementData( Login login,Register register);
       // HttpResponseMessage DeleteData(bool question, int id);
        HttpResponseMessage PutData(string token, FlashCard fs);
    }
}
