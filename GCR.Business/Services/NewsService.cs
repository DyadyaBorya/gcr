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

        public IQueryable<News> FetchAll()
        { 
            return newsRepository.Query.OrderByDescending(n=>n.ModifiedOn);
        }

        public IQueryable<NewsSummary> FetchArchiveSummaries()
        {
            return from n in FetchInternal(null, null, null, null)
                   group n by new { n.CreatedOn.Year, n.CreatedOn.Month } into g
                   select new NewsSummary 
                   { 
                       Date = g.FirstOrDefault().CreatedOn,
                       SummaryType = SummaryType.Month,
                       Count = g.Count()
                   };
        }

        public IQueryable<News> FetchArchive(DateTime startDate, DateTime endDate, int pageNumber, int numberOfEntries = 10)
        {
            if (startDate == DateTime.MinValue) throw new ArgumentException("startDate must be valid.");
            if (endDate == DateTime.MinValue) throw new ArgumentException("endDate must be valid.");
            if (pageNumber < 1) throw new ArgumentException("pageNumber can not be less than 1.");
            if (numberOfEntries < 1) throw new ArgumentException("numberOfEntries can not be less than 1.");

            return FetchInternal(startDate, endDate, pageNumber, numberOfEntries);
        }

        public IQueryable<News> FetchPaging(int pageNumber, int numberOfEntries = 10)
        {
            if (pageNumber < 1) throw new ArgumentException("pageNumber can not be less than 1.");
            if (numberOfEntries < 1) throw new ArgumentException("numberOfEntries can not be less than 1.");

            return FetchInternal(null, null, pageNumber, numberOfEntries);
        }

        public IQueryable<News> FetchRecent(int numberOfEntries = 10)
        {
            if (numberOfEntries < 1) throw new ArgumentException("numberOfEntries can not be less than 1.");

            return FetchInternal(null, null, null, numberOfEntries);
        }

        private IQueryable<News> FetchInternal(DateTime? startDate, DateTime? endDate, int? pageNumber, int? numberOfEntries)
        {
            var query = newsRepository.Query;

            if (startDate.HasValue)
            {
                query = query.Where(n => n.CreatedOn >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(n => n.CreatedOn < endDate.Value);
            }

            query = query.OrderByDescending(n => n.ModifiedOn);

            if (pageNumber.HasValue && numberOfEntries.HasValue)
            {
                query = query.Skip((pageNumber.Value - 1) * numberOfEntries.Value);
            }

            if (numberOfEntries.HasValue)
            {
                query = query.Take(numberOfEntries.Value);
            }

            return query;
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
