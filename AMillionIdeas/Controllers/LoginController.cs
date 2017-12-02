using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMillionIdeas.Models;
using System.Web.Security;
using AMillionIdeas.Services;
using AMillionIdeas.Security;

namespace AMillionIdeas.Controllers
{
    public class LoginController : Controller
    {
        private readonly IDBServices _IDBServices = new DBServices();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.InfoUsers user)
        {
            if (IsValid(user.UserName, user.UserPass))
            {
                //Added authentication mode="Forms" in Web.config
                string infoUserTicket = GetInfoUserTicket(user.UserName);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                    user.UserName,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    false,
                    infoUserTicket,   // Add User Id and Role, later we can retrive these data instead of read from DB
                    FormsAuthentication.FormsCookiePath);
                string encTicket = FormsAuthentication.Encrypt(ticket);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                Response.Redirect(FormsAuthentication.GetRedirectUrl(user.UserName, false));
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Login details are wrong.");
            }
            return View(user);
        }

        private bool IsValid(string UserName, string UserPass)
        {
            bool IsValid = false;     
            var userData = _IDBServices.GetInfoUserByNameContact(UserName);
            string passEncrypt = Crypto.Hash(UserPass, userData.UserSalt);
            if ((userData != null) && (userData.UserPass == passEncrypt))
            {
                IsValid = true;
            }
            return IsValid;
        }

        private string GetInfoUserTicket(string UserName)
        {
            string infoUserTicket = "0";
            string rol = "";
            string id = "";
            InfoUsers user = new InfoUsers();
            if ((user = _IDBServices.GetInfoUserByNameContact(UserName)) != null)
            {
                id = user.Id.ToString();
                rol = user.Rol.ToString();
                infoUserTicket = id + "|" + rol + "||";
            }
            return infoUserTicket;
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}