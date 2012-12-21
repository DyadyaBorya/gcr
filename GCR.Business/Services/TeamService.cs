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
    public class TeamService : ITeamService
    {
        private ITeamRepository teamRepository;

        public TeamService(ITeamRepository repo)
        {
            teamRepository = repo;
        }

        public IQueryable<Team> Fetch()
        { 
                return teamRepository.Query;
        }

        public Team GetById(int id)
        {
            return teamRepository.Query.SingleOrDefault(a => a.TeamId == id);
        }

        public void SaveTeam(Team team)
        {
            if (team.TeamId == 0)
            {
                teamRepository.Create(team);
            }
            else
            {
                teamRepository.Update(team);
            }

            teamRepository.SaveChanges();
        }

        public void DeleteTeam(Team team)
        {
            teamRepository.Delete(team);
            teamRepository.SaveChanges();
        }
    }
}
