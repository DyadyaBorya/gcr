using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCR.Core.Entities;

namespace GCR.Core.Services
{
    public interface IHomePageService
    {
        IEnumerable<HomePagePhoto> FetchPhotos();
        HomePagePhoto GetPhotoById(int id);
        void SavePhoto(HomePagePhoto photo);
        void SavePhotos(IEnumerable<HomePagePhoto> photo);
        void DeletePhoto(HomePagePhoto photo);
        void DeleteOrphanPhotos(Func<string, bool> validationFunc);
        string GetPhotoUploadPath();
    }
}
