using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GCR.Core.Services;
using GCR.Web.Models;

namespace GCR.Web.Controllers
{
    public class NewsController : BaseController
    {
        private INewsService newsService;

        public NewsController(INewsService service)
        {
            newsService = service;
            SetActiveTab(Tabs.News);
        }

        //
        // GET: /News/

        public ActionResult Index()
        {
            var articles = from n in newsService.FetchPaging(1)
                           select new NewsViewModel 
                           {
                               Article = n.Article,
                               Link = n.Link,
                               Summary = n.Summary,
                               Title = n.Title,
                               CreatedOn = n.CreatedOn,
                               ModifiedOn = n.ModifiedOn,
                           };
            return View(articles);
        }

        //
        // GET: /News/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Admin()
        {
            var articles = from n in newsService.FetchPaging(1)
                           select new NewsViewModel
                           {
                               NewsId = n.NewsId,
                               Summary = n.Summary,
                               Title = n.Title,
                               CreatedOn = n.CreatedOn,
                               ModifiedOn = n.ModifiedOn,
                           };
            return View(articles);
        }

        //
        // GET: /News/Create

        public ActionResult Create()
        {
            ViewBag.PageTitle = "Create New Article";
            ViewBag.Title = "Create Article";

            var model = new NewsViewModel();

            return View("CreateEdit", model);
        }

        //
        // POST: /News/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NewsViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var article = NewsViewModel.ToModel(model);
                    newsService.SaveNews(article);

                    return RedirectToAction("Admin");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
            }

            ViewBag.PageTitle = "Create New Article";
            ViewBag.Title = "Create Article";
            return View("CreateEdit", model);
        }

        //
        // GET: /News/Edit/5

        public ActionResult Edit(int id)
        {
            ViewBag.PageTitle = "Edit Article";
            ViewBag.Title = "Edit Article";

            var article = newsService.GetById(id);
            var model = NewsViewModel.ToViewModel(article);

            return View("CreateEdit", model);
        }

        //
        // POST: /News/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, NewsViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var article = newsService.GetById(id);
                    article = NewsViewModel.ToModel(model, article);
                    newsService.SaveNews(article);

                    return RedirectToAction("Admin");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
            }

            ViewBag.PageTitle = "Edit Article";
            ViewBag.Title = "Edit Article";
            return View("CreateEdit", model);
        }

        //
        // POST: /News/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var article = newsService.GetById(id);
                newsService.DeleteNews(article);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
            }

            return RedirectToAction("Admin");
        }
    }
}
