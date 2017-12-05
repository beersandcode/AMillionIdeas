using AMillionIdeas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMillionIdeas.Services;

namespace AMillionIdeas.Controllers
{

    public class BenchIdeasController : Controller
    {

        private readonly IDBServices _IDBServices = new DBServices();

        // GET: BenchIdeas
        public ActionResult Index()
        {
            IdeasBoardViewModel model = GetIdeasBoardViewModel();        
                   
            return View(model);
        }

        [HttpGet]
        public ActionResult _partialproduct(int pos)
        {

            var idea = _IDBServices.GetIdeaByPosition(pos);

            IdeaViewModel ideaViewModel = new IdeaViewModel();
            ideaViewModel.autor = idea.Autor;
            ideaViewModel.position = idea.Position;

            return PartialView("_IdeaModal",idea);

        }



        // GET: BenchIdeas/Details/5
        public ActionResult Details( )
        {

            //Ideas ideaf = _IDBServices.GetIdeaByPosition(pos);
            IdeaViewModel idea = new IdeaViewModel();
            idea.autor = "asdasd2";
            idea.title = "asdasd";

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


        private IdeasBoardViewModel GetIdeasBoardViewModel()
        {
            //take the info from the DB
            List<Ideas> ideas = _IDBServices.GetAllIdeas();

            //populate the view Model
            IdeasBoardViewModel model = new IdeasBoardViewModel();
            model.UsedPositions = new List<int?>();
            List<IdeaViewModel> ideasViewModelList = new List<IdeaViewModel>();

            foreach (var idea in ideas)
            {

                IdeaViewModel ideaViewModel = new IdeaViewModel();

                ideaViewModel.autor = idea.Autor;
                ideaViewModel.position = idea.Position;

                ideaViewModel.title = idea.Title;

                model.UsedPositions.Add(ideaViewModel.position);

                ideasViewModelList.Add(ideaViewModel);
            }

            model.ListIdeas = ideasViewModelList;

            //TODO: change or 
            model.MaxNumberOfIdeas = 100;

            model.NumberOfCreatedIdeas = ideas.Count();
            return model;
        }
    }
}
