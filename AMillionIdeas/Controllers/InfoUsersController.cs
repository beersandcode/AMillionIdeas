﻿using System;
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
using System.Web.Security;
using AMillionsIdeas.Security;

namespace AMillionIdeas.Controllers
{
    public class InfoUsersController : Controller
    {
        private readonly IDBServices _IDBServices = new DBServices(); 

        private AMillionIdeasDBEntities db = new AMillionIdeasDBEntities();

        // GET: InfoUsers
        [RoleAuthorization(Roles = "1")]
        public ActionResult Index()
        {
            return View(db.InfoUsers.ToList());
        }

        // GET: InfoUsers/AddUser   // Only the admin can use this method, the dofferent is that can set roles
        [RoleAuthorization(Roles = "1")]
        public ActionResult AddUser()
        {
            return View();
        }

        // POST: InfoUsers/AddUser
        [RoleAuthorization(Roles = "1")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser([Bind(Include = "Id,UserName,UserPass,Email,PhoneNumber,rol")] InfoUsers infoUser)
        {
            var infoUserTemp = _IDBServices.GetInfoUserByNameContact(infoUser.UserName);
            if (infoUserTemp == null) // If it´s null means that there is no another user with that name
            {
                if (ModelState.IsValid)
                {
                    string salt = Crypto.getSalt();
                    infoUser.UserSalt = salt;
                    string passEncrypt = Crypto.Hash(infoUser.UserPass, salt);
                    infoUser.UserPass = passEncrypt;
                    infoUser.Date = DateTime.Now;
                    _IDBServices.AddInfoUser(infoUser);
                    _IDBServices.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.msg = "Name User in use";
            }
            return View(infoUser);
        }

        // GET: InfoUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                string infoUserIdRolNewM = ticket.UserData.ToString();
                // It get user ID value from infoUserIdRolNewM
                int userId = Int32.Parse(infoUserIdRolNewM.Substring(0, infoUserIdRolNewM.IndexOf("|")));
                int roll = Int32.Parse(infoUserIdRolNewM.Substring((infoUserIdRolNewM.IndexOf("|")) + 1, (infoUserIdRolNewM.IndexOf("||") - infoUserIdRolNewM.IndexOf("|") - 1)));
                if ((id == userId) || (roll == 1))     // This way, only the user with has id can see his details
                {
                    InfoUsers infoUser = _IDBServices.GetInfoUser(id);
                    return View(infoUser);
                }
                else
                {
                    return HttpNotFound();  //TODO: add view reject or error
                }
            }
            return HttpNotFound();
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
                    infoUsers.Rol = 3; // Rol 1 = superUser, Rol 2 = admin, Rol 3 = common user
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

        public ActionResult ChangePass(int? id)
        {
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public ActionResult ChangePass(int id, string oldPass, string newPass, string newPass2)
        {
            ViewBag.id = id;
            InfoUsers infoUser = new InfoUsers();
            //infoUser = db.InfoUsers.Find(id);
            infoUser = _IDBServices.GetInfoUser(id);
            string salt = infoUser.UserSalt;
            string oldPassEncrypt = Crypto.Hash(oldPass, salt);
            salt = Crypto.getSalt();
            string passNewEncrypt = Crypto.Hash(newPass, salt);
            string passNew2Encrypt = Crypto.Hash(newPass2, salt);
            if (oldPassEncrypt == infoUser.UserPass)
            {
                if (oldPassEncrypt == passNewEncrypt)
                {
                    ViewBag.msgPass = "The same password";
                    return View();
                }
                else
                {
                    if (passNewEncrypt == passNew2Encrypt)
                    {
                        infoUser.UserSalt = salt;
                        infoUser.UserPass = passNewEncrypt;
                        //db.Entry(infoUser).State = EntityState.Modified;
                        _IDBServices.ModifiedInfoUser(infoUser);
                        //db.SaveChanges();
                        _IDBServices.SaveChanges();
                        return RedirectToAction("Logout", "Login");
                    }
                    else
                    {
                        ViewBag.msgPass = "It´s not the same password";
                        return View();
                    }

                }
            }
            else
            {
                ViewBag.msgPass = "Wrong Password";
                return View();
            }
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
