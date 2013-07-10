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
    public class ScheduleController : BaseController
    {
        private IScheduleService scheduleService;
        private ISecurityProvider securityProvider;
        private string uploadPath;

        public ScheduleController(IScheduleService service, ISecurityProvider security)
        {
            scheduleService = service;
            securityProvider = security;
            SetActiveTab(Tabs.Schedule);
        }
        //
        // GET: /Member/
        [AllowAnonymous]
        public ActionResult Index()
        {
            var members = from s in scheduleService.Fetch()
                          select new ScheduleViewModel
                          {
                              ScheduleId = s.ScheduleId,
                              SeasonId = s.SeasonId,
                              TeamId = s.TeamId,
                              Date = s.Date,
                              AtHome = s.AtHome
                          };
                        
            return View(members);
        }

        public ActionResult Admin()
        {
            var members = (from s in scheduleService.Fetch()
                           select new ScheduleViewModel
                          {
                              ScheduleId = s.ScheduleId,
                              SeasonId = s.SeasonId,
                              TeamId = s.TeamId,
                              Date = s.Date,
                              AtHome = s.AtHome
                          }).ToList();


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
                    //memberService.SaveMember(member);

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

            var schedule = scheduleService.GetById(id);
            var model = ScheduleViewModel.ToViewModel(schedule);

            return View("CreateEdit", model);
        }

        //
        // POST: /Member/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, ScheduleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var schedule = scheduleService.GetById(id);
                    schedule = ScheduleViewModel.ToModel(model, schedule);
                    scheduleService.SaveSchedule(schedule);

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
               // var member = memberService.GetById(id);
               // memberService.DeleteMember(member);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                this.LogError(ex);
            }

            return RedirectToAction("Admin");
        }
    }
}
