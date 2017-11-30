using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AMillionIdeas.Models;

namespace AMillionIdeas.Controllers
{
    public class InfoUsersController : Controller
    {
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
            if (ModelState.IsValid)
            {
                //add data, salt, hash 

                infoUsers.Date = DateTime.Now;
                infoUsers.Rol = 1;
                db.InfoUsers.Add(infoUsers);
                db.SaveChanges();
                return RedirectToAction("Index");
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
