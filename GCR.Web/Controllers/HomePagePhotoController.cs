using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GCR.Core;
using GCR.Core.Entities;
using GCR.Core.Security;
using GCR.Core.Services;
using GCR.Web.Models;
using GCR.Web.Infrastructure;
using System.IO;


namespace GCR.Web.Controllers
{
    public class HomePagePhotoController : BaseController
    {
        private IHomePageService homePageService;
        private ISecurityProvider securityProvider;
        private string uploadPath;

        public HomePagePhotoController(IHomePageService service, ISecurityProvider security)
        {
            homePageService = service;
            securityProvider = security;
            SetActiveTab(Tabs.Home);
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            uploadPath = Configuration.UploadPath + "Photos/Homepage";

            string str = securityProvider.EncryptData(uploadPath);
            ViewBag.UploadPath = "~/ImageUpload.aspx?w=960&h=450&p=" + Url.Encode(str);

        }

        public ActionResult Admin()
        {
            var photos = (from m in homePageService.FetchPhoto()
                           select new HomePagePhotoViewModel
                          {
                              HomePagePhotoId = m.HomePagePhotoId,
                              DisplayOrder = m.DisplayOrder,
                              PhotoPath = m.PhotoPath
                          }).ToList();


            DeleteOrphanPhotos(photos);
            return View(photos);
        }

        [HttpPost]
        public ActionResult SaveOrder(IEnumerable<HomePagePhotoOrderViewModel> photosVM)
        {
            if (photosVM != null)
            {
                var photos = homePageService.FetchPhoto().ToList();
                foreach (var photoVM in photosVM)
                {
                    var photo = photos.FirstOrDefault(p => p.HomePagePhotoId == photoVM.Id);
                    if (photo != null)
                    {
                        photo.DisplayOrder = photoVM.Order;
                        homePageService.SavePhoto(photo);
                    }
                }
                return Json(new { Status = MessageMode.Success, Message = "Photo Display Order Updated!" });
            }
            else
            {
                return Json(new { Status = MessageMode.Error, Message = "Something went Wrong!" });
            }
        }

        //
        // GET: /Member/Create

        public ActionResult Create()
        {
            ViewBag.PageTitle = "Create Home Page Photo";
            ViewBag.Title = "Create Home Page Photo";

            var model = new HomePagePhotoViewModel();

            return View(model);
        }

        //
        // POST: /Member/Create

        [HttpPost]
        public ActionResult Create(HomePagePhotoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var photo = HomePagePhotoViewModel.ToModel(model);
                    homePageService.SavePhoto(photo);

                    return RedirectToAction("Admin");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex);
            }

            ViewBag.PageTitle = "Create Home Page Photo";
            ViewBag.Title = "Create Home Page Photo";
            return View(model);
        }


        //
        // POST: /Member/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var photo = homePageService.GetPhotoById(id);
                homePageService.DeletePhoto(photo);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
            }

            return RedirectToAction("Admin");
        }

        private void DeleteOrphanPhotos(IEnumerable<HomePagePhotoViewModel> photos)
        {
            Func<string, bool> func = p => photos.Any(m => Path.GetFileName(m.PhotoPath) == Path.GetFileName(p));
            homePageService.DeleteOrphanPhotos(func);
        }
    }
}
