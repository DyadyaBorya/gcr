using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCR.Core.Entities;

namespace GCR.Core.Services
{
    public interface IScheduleService
    {
        IEnumerable<Schedule> Fetch { get; }
        Schedule GetById(int id);
        void SaveSchedule(Schedule schedule);
        void DeleteSchedule(Schedule schedule);
    }
}
