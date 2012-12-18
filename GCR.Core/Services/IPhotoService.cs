using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using GCR.Core;
using GCR.Core.Entities;
using GCR.Core.Repositories;
using GCR.Core.Services;

namespace GCR.Core.Services
{
    public interface IPhotoService 
    {
        void Initialize(string path);

        bool DeletePhoto(string path);

        void DeleteOrphanPhotos(Func<string, bool> validationFunc);
      
    }
}
