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

        public List<Ideas> GetAllIdeas()
        {
            return db.Ideas.ToList();
        }

        public Ideas GetIdeaByPosition(int pos)
        {
            return db.Ideas.Where(i => i.Position == pos).FirstOrDefault();
        }

        public InfoUsers GetInfoUser(int? UserIdreported) 
        {
            return db.InfoUsers.Find(UserIdreported);
        }

    }
}