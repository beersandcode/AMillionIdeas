using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMillionIdeas.Controllers
{
    public class MessagesController : Controller
    {

        public ActionResult SendMessage(int id, int idDestUser) // user id registered, 
        {
            return View();
        }

        public ActionResult DeleteMessage(int id) 
        {
            return View();
        }


        public ActionResult ShowMessages(int id) 
        {
            // show all the messages, order by users or chats
            return View();
        }
}