using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCR.Core.Entities;
using GCR.Core.Repositories;
using GCR.Core.Services;

namespace GCR.Business.Services
{
    public class ScheduleService : IScheduleService
    {
        private IScheduleRepository scheduleRepository;

        public ScheduleService(IScheduleRepository repo)
        {
            scheduleRepository = repo;
        }

        public IEnumerable<Schedule> Fetch 
        { 
            get 
            {
                return scheduleRepository.Query;
            }
        }

        public Schedule GetById(int id)
        {
            return scheduleRepository.Query.SingleOrDefault(a => a.ScheduleId == id);
        }

        public void SaveSchedule(Schedule schedule)
        {
            if (schedule.ScheduleId == 0)
            {
                scheduleRepository.Create(schedule);
            }
            else
            {
                scheduleRepository.Update(schedule);
            }

            scheduleRepository.SaveChanges();
        }

        public void DeleteSchedule(Schedule schedule)
        {
            scheduleRepository.Delete(schedule);
            scheduleRepository.SaveChanges();
        }
    }
}
