using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GCR.Core;
using GCR.Core.Entities;
using GCR.Core.Security;
using GCR.Core.Services;
using GCR.Web.Infrastructure;
using GCR.Web.Models;

namespace GCR.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MemberController : BaseController
    {
        private IMemberService memberService;
        private ISecurityProvider securityProvider;
        private string uploadPath;

        public MemberController(IMemberService service, ISecurityProvider security)
        {
            memberService = service;
            securityProvider = security;
            SetActiveTab(Tabs.Member);
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            uploadPath = memberService.GetPhotoUploadPath();

            string str = securityProvider.EncryptData(uploadPath);
            ViewBag.UploadPath = "~/ImageUpload.aspx?w=150&h=200&p=" + Url.Encode(str);

        }
        //
        // GET: /Member/
        [AllowAnonymous]
        [HtmlAllowed]
        public ActionResult Index()
        {
            var members = from m in memberService.FetchActive()
                          select new MemberViewModel
                          {
                              MemberId = m.MemberId,
                              Bio = m.Bio,
                              FirstName = m.FirstName,
                              LastName = m.LastName,
                              MemberSince = m.MemberSince,
                              Photo = m.Photo
                          };
                        
            return View(members);
        }

        public ActionResult Admin()
        {
            var members = (from m in memberService.FetchAll()
                          select new MemberViewModel
                          {
                              MemberId = m.MemberId,
                              FirstName = m.FirstName,
                              LastName = m.LastName,
                              MemberSince = m.MemberSince,
                              IsActive = m.IsActive,
                              Photo = m.Photo
                          }).ToList();


            DeleteOrphanPhotos(members);
            return View(members);
        }

        //
        // GET: /Member/Create
        public ActionResult Create()
        {
            ViewBag.PageTitle = "Create New Member";
            ViewBag.Title = "Create Member";

            var model = new MemberViewModel();
            model.IsActive = true;

            return View("CreateEdit", model);
        }

        //
        // POST: /Member/Create

        [HttpPost]
        public ActionResult Create(MemberViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var member = MemberViewModel.ToModel(model);
                    memberService.SaveMember(member);

                    return RedirectToAction("Admin");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex);
                this.LogError(ex);
            }

            ViewBag.PageTitle = "Create New Member";
            ViewBag.Title = "Create Member";
            return View("CreateEdit", model);
        }

        //
        // GET: /Member/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.PageTitle = "Edit Member";
            ViewBag.Title = "Edit Member";

            var member = memberService.GetById(id);
            var model = MemberViewModel.ToViewModel(member);

            return View("CreateEdit", model);
        }

        //
        // POST: /Member/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, MemberViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var member = memberService.GetById(id);
                    member = MemberViewModel.ToModel(model, member);
                    memberService.SaveMember(member);

                    return RedirectToAction("Admin");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                this.LogError(ex);
            }

            ViewBag.PageTitle = "Edit Member";
            ViewBag.Title = "Edit Member";
            return View("CreateEdit", model);
        }


        //
        // POST: /Member/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var member = memberService.GetById(id);
                memberService.DeleteMember(member);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                this.LogError(ex);
            }

            return RedirectToAction("Admin");
        }

        private void DeleteOrphanPhotos(IEnumerable<MemberViewModel> members)
        {
            Func<string, bool> func = p => members.Any(m => Path.GetFileName(m.Photo) == Path.GetFileName(p));
            memberService.DeleteOrphanPhotos(func);
        }
    }
}
