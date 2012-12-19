using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GCR.Web.Models
{
    public class ScheduleViewModel
    {
        public int ScheduleId { get; set; }

        public int? SeasonId { get; set; }
        public int? TeamId { get; set; }
        public System.DateTime? Date { get; set; }
        public bool AtHome { get; set; }

        public static ScheduleViewModel ToViewModel(GCR.Core.Entities.Schedule schedule, ScheduleViewModel model = null)
        {
            if (model == null)
            {
                model = new ScheduleViewModel();
            }
            model.SeasonId = schedule.SeasonId;
            model.TeamId = schedule.TeamId;
            model.Date = schedule.Date;
            model.AtHome = schedule.AtHome;

            return model;
        }

        public static GCR.Core.Entities.Schedule ToModel(ScheduleViewModel model, GCR.Core.Entities.Schedule schedule = null)
        {
            if (schedule == null)
            {
                schedule = new GCR.Core.Entities.Schedule();
            }

            model.SeasonId = schedule.SeasonId;
            model.TeamId = schedule.TeamId;
            model.Date = schedule.Date;
            model.AtHome = schedule.AtHome;

            return schedule;
        }
    }
}