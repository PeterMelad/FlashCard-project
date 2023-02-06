using Interfaces;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Implementation
{
    public class Mediator : IMediator
    {
        HttpClientHandler handler;
        HttpClient client;
        IConfiguration configuration;
        public Mediator(IConfiguration configuration)
        {
            handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback +=
                            (sender, certificate, chain, errors) =>
                            {
                                return true;
                            };
            client = new HttpClient(handler);
            this.configuration = configuration;
        }

        //public HttpResponseMessage DeleteData(bool question, int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public HttpResponseMessage GetData(int id)
        //{
        //    if (id==0)
        //    {
        //        return (client.GetAsync(configuration["baseaddressapi"] + "api/FlashCard").Result);
        //    }
        //    else
        //    {
        //       return(client.GetAsync(configuration["baseaddressapi"] + "api/FlashCard/" + id).Result);
        //    }
        //}
       
        public HttpResponseMessage GetData(string token,int id)
        {
            client.DefaultRequestHeaders.Authorization =new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            if (id==0)
            {
                return (client.GetAsync(configuration["baseaddressapi"] + "api/FlashCard").Result);
            }
            else
            {
               return(client.GetAsync(configuration["baseaddressapi"] + "api/FlashCard/" + id).Result);
            }
        }

        public HttpResponseMessage PostData(string token, FlashCard fs,ModelQuestion q)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            if (fs ==null)
            {
                return (client.PostAsJsonAsync<ModelQuestion>(configuration["baseaddressapi"] + "api/FlashCard/AddSpecificQuestion", q).Result);
            }
            else
            {
              return (client.PostAsJsonAsync<FlashCard>(configuration["baseaddressapi"] + "api/FlashCard", fs).Result);
            }
        }

        public HttpResponseMessage PostManagementData(Login login, Register register)
        {
            if (login != null)
            {
                return (client.PostAsJsonAsync(configuration["baseaddressapi"] + "api/Security/Login", login).Result);
            }
            else
            {
                return (client.PostAsJsonAsync<Register>(configuration["baseaddressapi"] + "api/Security/Register", register).Result);
            }
        }

        public HttpResponseMessage PutData(string token, FlashCard fs)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return (client.PutAsJsonAsync<FlashCard>(configuration["baseaddressapi"] + "api/FlashCard", fs).Result);
        }
    }
}
