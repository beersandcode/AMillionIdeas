﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AMillionIdeas.Models;

namespace AMillionsIdeas.Security
{
    public class RoleAuthorizationAttribute : AuthorizeAttribute
    {
        private AMillionIdeasDBEntities db = new AMillionIdeasDBEntities();
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string rolAllow = Roles.ToString(); // this parameter (Roles) came from controller, example [RoleAuthorization(Roles = "1")]
            bool authorize = false;
            string UserName = HttpContext.Current.User.Identity.Name;
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                string infoUserIdRol = ticket.UserData.ToString();
                // It get user ID value from infoUserIdRol
                int userId = Int32.Parse(infoUserIdRol.Substring(0, infoUserIdRol.IndexOf("|")));
                string rol;
                InfoUsers user = new InfoUsers();
                //Also we can read the roll value from the cookie
                if ((user = db.InfoUsers.Where(a => a.Id.Equals(userId)).FirstOrDefault()) != null)
                {
                    rol = user.Rol.ToString();
                    if (rol == rolAllow)     // 1 = Admin
                        return true;
                }
            }
            return authorize;
        } 
    }
}