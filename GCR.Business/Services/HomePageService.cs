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

        public IQueryable<HomePagePhoto> FetchPhotos()
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
                photo.DisplayOrder = GetNextDisplayOrder();
            }
            else
            {
                homePageRepository.Update(photo);
            }

            homePageRepository.SaveChanges();

            EnsureDisplayOrders();
            homePageRepository.SaveChanges();
        }

        public void SavePhotos(IEnumerable<HomePagePhoto> photos)
        {
            using (var scope = new TransactionScope())
            {
                foreach (var photo in photos)
                {
                    if (photo.HomePagePhotoId == 0)
                    {
                        homePageRepository.Create(photo);
                    }
                    else
                    {
                        homePageRepository.Update(photo);
                    }
                }
                homePageRepository.SaveChanges();

                EnsureDisplayOrders(photos.OrderBy(d=>d.DisplayOrder));
                homePageRepository.SaveChanges();
                scope.Complete();
            }
        }

        public void DeletePhoto(HomePagePhoto photo)
        {
            using (var scope = new TransactionScope())
            {
                homePageRepository.Delete(photo);
                homePageRepository.SaveChanges();

                EnsureDisplayOrders();
                homePageRepository.SaveChanges();

                photoService.DeletePhoto(photo.PhotoPath);
                scope.Complete();
            }
        }

        public void DeleteOrphanPhotos(Func<string, bool> validationFunc)
        {
            photoService.DeleteOrphanPhotos(validationFunc);
        }

        public string GetPhotoUploadPath()
        {
            return uploadPath;
        }

        private void EnsureDisplayOrders()
        {
            EnsureDisplayOrders(FetchPhotos().ToList());

        }

        private void EnsureDisplayOrders(IEnumerable<HomePagePhoto> photos)
        {
            int count = 1;
            foreach (var photo in photos)
            {
                photo.DisplayOrder = count;
                count++;
            }
        }

        private int GetNextDisplayOrder()
        {
            int? lastOrder = (from p in homePageRepository.Query
                              orderby p.DisplayOrder descending
                              select p.DisplayOrder).FirstOrDefault();

            return lastOrder.HasValue ? lastOrder.Value + 1 : 1;
        }
    }
}
