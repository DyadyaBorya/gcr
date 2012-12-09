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
        IEnumerable<News> Fetch { get; }
        News GetById(int id);
        void SaveNews(News news);
        void DeleteNews(News news);
    }
}
