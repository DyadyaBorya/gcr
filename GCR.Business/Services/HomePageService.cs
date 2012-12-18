using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCR.Core;
using GCR.Core.Entities;
using GCR.Core.Repositories;
using GCR.Core.Services;

namespace GCR.Business.Services
{
    public class HomePageService : IHomePageService
    {
        private IHomePagePhotoRepository homePageRepository;
        private IPhotoService photoService;
        private string uploadPath;

        public HomePageService(IHomePagePhotoRepository repo, IPhotoService service)
        {
            homePageRepository = repo;
            photoService = service;
            uploadPath = Configuration.UploadPath + "Photos/Homepage";
            photoService.Initialize(uploadPath);

        }

        public IEnumerable<HomePagePhoto> FetchPhoto()
        {
            return homePageRepository.Query.
                OrderBy(a => a.DisplayOrder);
        }

        public HomePagePhoto GetPhotoById(int id)
        {
            return homePageRepository.Query.SingleOrDefault(a => a.HomePagePhotoId == id);
        }

        public void SavePhoto(HomePagePhoto photo)
        {
            if (photo.HomePagePhotoId == 0)
            {
                homePageRepository.Create(photo);
            }
            else
            {
                homePageRepository.Update(photo);
            }

            homePageRepository.SaveChanges();
        }

        public void DeletePhoto(HomePagePhoto photo)
        {
            homePageRepository.Delete(photo);
            homePageRepository.SaveChanges();
        }

        public void DeleteOrphanPhotos(Func<string, bool> validationFunc)
        {
            photoService.DeleteOrphanPhotos(validationFunc);
        }

        public string GetPhotoUploadPath()
        {
            return uploadPath;
        }
    }
}
