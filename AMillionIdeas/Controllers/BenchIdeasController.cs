using AMillionIdeas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMillionIdeas.Controllers
{
    public class BenchIdeasController : Controller
    {
        // GET: BenchIdeas
        public ActionResult Index()
        {
            List<IdeaViewModel> Model  = new List<IdeaViewModel> ();
            IdeaViewModel idea = new IdeaViewModel();
            idea.autor = "Javi";
            idea.title = "Create a web page to create Ideas";
            Model.Add(idea);

            return View(Model);
        }

        [HttpGet]
        public ActionResult _partialproduct(int id)
        {

            IdeaViewModel idea = new IdeaViewModel();
            idea.autor = "Javi";
            idea.title = "Create a web page to create Ideas";

            return View("_IdeaModal",idea);

        }



        // GET: BenchIdeas/Details/5
        public ActionResult Details()
        {
            IdeaViewModel idea = new IdeaViewModel();
            idea.autor = "Javi";
            idea.title = "Create a web page to create Ideas";

            return PartialView("_IdeaModal", idea);
        }

        // GET: BenchIdeas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BenchIdeas/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BenchIdeas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BenchIdeas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BenchIdeas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BenchIdeas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
