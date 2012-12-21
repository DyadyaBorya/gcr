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

        public IEnumerable<News> FetchAll()
        { 
            return newsRepository.Query.OrderByDescending(n=>n.ModifiedOn);
        }

        public IEnumerable<News> FetchPaging(int pageNumber, int numberOfEntries = 10)
        {
            if (pageNumber < 1) throw new ArgumentException("pageNumber can not be less than 1.");
            if (numberOfEntries < 1) throw new ArgumentException("numberOfEntries can not be less than 1.");

            return FetchAll().Skip((pageNumber-1) * numberOfEntries).Take(numberOfEntries);
        }

        public IEnumerable<News> FetchRecent(int numberOfEntries = 10)
        {
            if (numberOfEntries < 1) throw new ArgumentException("numberOfEntries can not be less than 1.");

            return FetchAll().Take(numberOfEntries);
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
