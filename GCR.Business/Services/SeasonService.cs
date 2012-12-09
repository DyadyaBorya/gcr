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
    public class SeasonService : ISeasonService
    {
        private ISeasonRepository seasonRepository;

        public SeasonService(ISeasonRepository repo)
        {
            seasonRepository = repo;
        }

        public IEnumerable<Season> Fetch 
        { 
            get 
            {
                return seasonRepository.Query;
            }
        }

        public Season GetById(int id)
        {
            return seasonRepository.Query.SingleOrDefault(a => a.SeasonId == id);
        }

        public void SaveSeason(Season season)
        {
            if (season.SeasonId == 0)
            {
                seasonRepository.Create(season);
            }
            else
            {
                seasonRepository.Update(season);
            }

            seasonRepository.SaveChanges();
        }

        public void DeleteSeason(Season season)
        {
            seasonRepository.Delete(season);
            seasonRepository.SaveChanges();
        }
    }
}
