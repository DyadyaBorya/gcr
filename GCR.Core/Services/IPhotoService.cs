using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCR.Core.Entities;

namespace GCR.Core.Services
{
    public interface IPhotoService
    {
        IEnumerable<HomePagePhoto> FetchHomePagePhoto { get; }
        HomePagePhoto GetById(int id);
        void SaveHomePagePhoto(HomePagePhoto photo);
        void DeleteHomePagePhoto(HomePagePhoto photo);
    }
}
