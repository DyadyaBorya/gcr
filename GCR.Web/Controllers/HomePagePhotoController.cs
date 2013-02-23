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
    [Authorize]
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
            var photos = (from m in homePageService.FetchPhotos()
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
                var photos = homePageService.FetchPhotos().ToList();
                foreach (var photoVM in photosVM)
                {
                    var photo = photos.FirstOrDefault(p => p.HomePagePhotoId == photoVM.Id);
                    if (photo != null)
                    {
                        photo.DisplayOrder = photoVM.Order;
                    }
                }
                homePageService.SavePhotos(photos);

                var sm = new StatusMessage(MessageMode.Success, "New Order Saved Successfully!", Url.Action("Admin"));
                this.ShowMessageAfterRedirect(sm);
                return Json(sm);
            }
            else
            {
                return Json(new StatusMessage(MessageMode.Error, "Order could not be Saved!"));
            }
        }

        [AllowAnonymous]
        public ActionResult HomePage()
        {
            var photos = homePageService.FetchPhotos().ToList();
            var model = new List<HomePagePhotoViewModel>(photos.Count);
            for (int i = 1; i <= photos.Count; i++)
			{
                var photo = photos[i - 1];
                if (i < photos.Count)
                {
                    model.Add(HomePagePhotoViewModel.ToViewModel(photo));
                }
                else
                {
                    model.Insert(0, HomePagePhotoViewModel.ToViewModel(photo));
                }
			}

            return View("_HomePage", model);
        }

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

                    return PartialView("_PhotoPartial", HomePagePhotoViewModel.ToViewModel(photo));
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex);
                this.LogError(ex);
            }

            return Json(new { Status = MessageMode.Error, Message = "Error occurred until saving photo!" });
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
                this.LogError(ex);
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
