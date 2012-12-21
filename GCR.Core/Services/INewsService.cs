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
        IEnumerable<News> FetchAll();
        IEnumerable<News> FetchRecent(int numberOfEntries = 10);
        IEnumerable<News> FetchPaging(int pageNumber, int numberOfEntries = 10);
        News GetById(int id);
        void SaveNews(News news);
        void DeleteNews(News news);
    }
}
