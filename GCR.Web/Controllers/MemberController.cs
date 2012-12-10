using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GCR.Core.Services;
using GCR.Web.Models.Member;

namespace GCR.Web.Controllers
{
    public class MemberController : Controller
    {
        private IMemberService memberService;

        public MemberController(IMemberService service)
        {
            memberService = service;
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
            return View();
        }

        //
        // GET: /Member/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Member/Create

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

        //
        // GET: /Member/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Member/Edit/5

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


        //
        // POST: /Member/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
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
