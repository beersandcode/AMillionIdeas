using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMillionIdeas.Models;

namespace AMillionIdeas.Services
{
    public interface IDBServices
    {
        //commons
        void SaveChanges();

        //InfoUserController
        InfoUsers GetInfoUserByNameContact(string nameContact);
        void AddInfoUser(InfoUsers infoUser);
        void ModifiedInfoUser(InfoUsers infoUser);
        InfoUsers GetInfoUser(int? UserIdreported);

        List<Ideas> GetAllIdeas();
        Ideas GetIdeaByPosition(int pos);

    }
}