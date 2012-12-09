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
    public class NewsService : INewsService
    {
        private INewsRepository newsRepository;

        public NewsService(INewsRepository repo)
        {
            newsRepository = repo;
        }

        public IEnumerable<News> Fetch 
        { 
            get 
            { 
                return newsRepository.Query;
            }
        }

        public News GetById(int id)
        {
            return newsRepository.Query.SingleOrDefault(a => a.NewsId == id);
        }

        public void SaveNews(News news)
        {
            if (news.NewsId == 0)
            {
                newsRepository.Create(news);
            }
            else
            {
                newsRepository.Update(news);
            }

            newsRepository.SaveChanges();
        }

        public void DeleteNews(News news)
        {
            newsRepository.Delete(news);
            newsRepository.SaveChanges();
        }
    }
}
