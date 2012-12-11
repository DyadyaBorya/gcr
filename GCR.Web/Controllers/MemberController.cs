using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GCR.Core.Entities;
using GCR.Core.Services;
using GCR.Web.Models.Member;

namespace GCR.Web.Controllers
{
    public class MemberController : BaseController
    {
        private IMemberService memberService;

        public MemberController(IMemberService service)
        {
            memberService = service;
            SetActiveTab(Tabs.Member);
        }
        //
        // GET: /Member/

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
            var members = from m in memberService.FetchAll()
                          select new MemberViewModel
                          {
                              MemberId = m.MemberId,
                              FirstName = m.FirstName,
                              LastName = m.LastName,
                              MemberSince = m.MemberSince,
                              IsActive = m.IsActive
                          };

            return View(members);
        }

        //
        // GET: /Member/Create

        public ActionResult Create()
        {
            var members = new MemberViewModel();

            return View(members);
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
            }

            return View(model);
        }

        //
        // GET: /Member/Edit/5

        public ActionResult Edit(int id)
        {
            var member = memberService.GetById(id);

            return View(member);
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
                    var member = MemberViewModel.ToModel(model);
                    member.MemberId = id;
                    memberService.SaveMember(member);

                    return RedirectToAction("Admin");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
            }

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
                var member = memberService.GetById(id);
                memberService.DeleteMember(member);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
            }

            return RedirectToAction("Admin");
        }
    }
}
