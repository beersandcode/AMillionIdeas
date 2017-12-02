using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AMillionIdeas.Models;

namespace AMillionIdeas.Services
{
    public class DBServices:IDBServices
    {
        private AMillionIdeasDBEntities db = new AMillionIdeasDBEntities();

        //Commons
        public void SaveChanges()
        {
            db.SaveChanges();
        }

        //InfoUserController
        public InfoUsers GetInfoUserByNameContact(string nameContact)
        {
            return db.InfoUsers.Where(o => o.UserName == nameContact).FirstOrDefault(); 
        }

        public void AddInfoUser(InfoUsers infoUser)
        {
            db.InfoUsers.Add(infoUser);
        }

        public void ModifiedInfoUser(InfoUsers infoUser)
        {
            db.Entry(infoUser).State = EntityState.Modified;
        }


    }
}