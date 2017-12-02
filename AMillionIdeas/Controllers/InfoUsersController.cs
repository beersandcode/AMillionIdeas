using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AMillionIdeas.Models;
using AMillionIdeas.Security;
using AMillionIdeas.Services;

namespace AMillionIdeas.Controllers
{
    public class InfoUsersController : Controller
    {
        private readonly IDBServices _IDBServices = new DBServices(); 

        private AMillionIdeasDBEntities db = new AMillionIdeasDBEntities();

        // GET: InfoUsers
        public ActionResult Index()
        {
            return View(db.InfoUsers.ToList());
        }

        // GET: InfoUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InfoUsers infoUsers = db.InfoUsers.Find(id);
            if (infoUsers == null)
            {
                return HttpNotFound();
            }
            return View(infoUsers);
        }

        // GET: InfoUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InfoUsers/Create
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,UserPass,Email,PhoneNumber,Rol,Date,UserSalt")] InfoUsers infoUsers)
        {
            var infoUserTemp = _IDBServices.GetInfoUserByNameContact(infoUsers.UserName);
            if (infoUserTemp == null) // If it´s null means that there is no another user with that name
            {
                if (ModelState.IsValid)
                {
                    string salt = Crypto.getSalt();
                    infoUsers.UserSalt = salt;
                    string passEncrypt = Crypto.Hash(infoUsers.UserPass, salt);
                    infoUsers.UserPass = passEncrypt;
                    infoUsers.Date = DateTime.Now;
                    infoUsers.Rol = 1;
                    //db.InfoUsers.Add(infoUsers);
                    _IDBServices.AddInfoUser(infoUsers);
                    //db.SaveChanges();
                    _IDBServices.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.msg = "Name User in use";
            }
            return View(infoUsers);
        }

        // GET: InfoUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InfoUsers infoUsers = db.InfoUsers.Find(id);
            if (infoUsers == null)
            {
                return HttpNotFound();
            }
            return View(infoUsers);
        }

        // POST: InfoUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,UserPass,Email,PhoneNumber,Rol,Date,UserSalt")] InfoUsers infoUsers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(infoUsers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(infoUsers);
        }

        // GET: InfoUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InfoUsers infoUsers = db.InfoUsers.Find(id);
            if (infoUsers == null)
            {
                return HttpNotFound();
            }
            return View(infoUsers);
        }

        // POST: InfoUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InfoUsers infoUsers = db.InfoUsers.Find(id);
            db.InfoUsers.Remove(infoUsers);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
