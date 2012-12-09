using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCR.Core.Entities;

namespace GCR.Core.Services
{
    public interface ISeasonService
    {
        IEnumerable<Season> Fetch { get; }
        Season GetById(int id);
        void SaveSeason(Season season);
        void DeleteSeason(Season season);
    }
}
