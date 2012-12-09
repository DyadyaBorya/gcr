using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCR.Core.Entities;
using GCR.Core.Repositories;

namespace GCR.Model.Repositories
{
    public class NewsRepository : Repository<News>, INewsRepository
    {
    }
}
