using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GCR.Core.Services;
using GCR.Web.Infrastructure;
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

        public ActionResult Index(int? page)
        {
            if (!page.HasValue)
            {
                page = 1;
            }
            var articles = from n in newsService.FetchPaging(page.Value)
                           select new NewsViewModel 
                           {
                               NewsId = n.NewsId,
                               Article = n.Article,
                               Summary = n.Summary,
                               Title = n.Title,
                               CreatedOn = n.CreatedOn,
                               ModifiedOn = n.ModifiedOn,
                           };
            return View(articles);
        }

        public ActionResult Archive(string year, string month, string day, int? page)
        {
            var dates = GetDates(year, month, day);
            if (!page.HasValue)
            {
                page = 1;
            }

            var articles = from n in newsService.FetchArchive(dates.Item1, dates.Item2, page.Value)
                           select new NewsViewModel
                           {
                               NewsId = n.NewsId,
                               Article = n.Article,
                               Summary = n.Summary,
                               Title = n.Title,
                               CreatedOn = n.CreatedOn,
                               ModifiedOn = n.ModifiedOn,
                           };

            return View("Index", articles);
        }

        //
        // GET: /News/Details/5

        public ActionResult Details(int id)
        {
            var article = newsService.GetById(id);
            var model = NewsViewModel.ToViewModel(article);
            return View(model);
        }

        public ActionResult ArchiveSummary()
        {
            var sums = newsService.FetchArchiveSummaries().ToList();
            var model = new List<NewsArchiveViewModel>(sums.Count());
            foreach (var sum in sums)
            {
                string yearString = sum.Date.Year.ToString();
                string monthString = sum.Date.ToString("MM");

                model.Add(new NewsArchiveViewModel()
                {
                    Count = sum.Count,
                    Name = sum.Date.ToString("MMMM yyyy"),
                    Link = Url.RouteUrl("Archive", new { year = yearString, month = monthString })
                        
                });
            }

            return View("_Archive", model);
        }

        public ActionResult Recent()
        {
            var article = from n in newsService.FetchRecent()
                        select new NewsViewModel 
                        {
                            NewsId = n.NewsId,
                            Summary = n.Summary,
                            Title = n.Title,
                            CreatedOn = n.CreatedOn
                        };

            return View(article);
        }

        public ActionResult Admin()
        {
            var articles = from n in newsService.FetchAll()
                           select new NewsViewModel
                           {
                               NewsId = n.NewsId,
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
                this.LogError(ex);
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
                this.LogError(ex);
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
                this.LogError(ex);
            }

            return RedirectToAction("Admin");
        }

        private Tuple<DateTime, DateTime> GetDates(string year, string month, string day)
        {
            int y = 2012;
            int m = 1;
            int d = 1;
            bool monthSpecified = false;
            bool daySpecified = false;
            DateTime startDate;
            DateTime endDate;

            if (!int.TryParse(year, out y))
            {
                y = 2012;
            }
            if (int.TryParse(month, out m))
            {
                monthSpecified = true;
            }
            else
            {
                m = 1;
            }
            if (int.TryParse(day, out d))
            {
                daySpecified = true;
            }
            else
            {
                d = 1;
            }

            startDate = new DateTime(y, m, d);

            if (daySpecified)
            {
                endDate = startDate.AddDays(1);
            }
            else if (monthSpecified)
            {
                endDate = startDate.AddMonths(1);
            }
            else
            {
                endDate = startDate.AddYears(1);
            }
            return new Tuple<DateTime, DateTime>(startDate, endDate);
        }
    }
}
