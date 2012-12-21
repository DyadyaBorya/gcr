using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCR.Core.Entities;

namespace GCR.Core.Services
{
    public interface INewsService
    {
        IQueryable<News> FetchAll();
        IQueryable<News> FetchRecent(int numberOfEntries = 10);
        IQueryable<News> FetchPaging(int pageNumber, int numberOfEntries = 10);
        IQueryable<News> FetchArchive(DateTime startDate, DateTime endDate, int pageNumber, int numberOfEntries = 10);
        IQueryable<NewsSummary> FetchArchiveSummaries();
        News GetById(int id);
        void SaveNews(News news);
        void DeleteNews(News news);
    }
}
