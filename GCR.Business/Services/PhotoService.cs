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

namespace GCR.Business.Services
{
    internal class PhotoService : IPhotoService
    {
        private string resolvedPath;
        private string phyiscalPath;
        private bool initCalled;

        public PhotoService()
        {

        }

        public void Initialize(string path)
        {
            resolvedPath = ResolvePath(path);
            phyiscalPath = GetPhyiscalPath(resolvedPath);
            initCalled = true;
        }


        public bool DeletePhoto(string path)
        {
            if (!initCalled) throw new InvalidOperationException("Initialize has not been called.");

            string fileToDelete = Path.Combine(phyiscalPath, Path.GetFileName(path));
            if (PathExists(fileToDelete))
            {
                File.Delete(fileToDelete);
                return true;
            }
            return false;
        }

        public void DeleteOrphanPhotos(Func<string, bool> validationFunc)
        {
            if (!initCalled) throw new InvalidOperationException("Initialize has not been called.");

            Action action = () => DeleteOrphanPhotosInternal(validationFunc);
            Task.Run(action);
        }

        private void DeleteOrphanPhotosInternal(Func<string, bool> validationFunc)
        {
            string[] strArray = Directory.GetFiles(phyiscalPath);
            int num = 0;

            DateTime time = DateTime.Now.AddMinutes(Configuration.PhotoFileLifeTime * -1);

            for (int i = 0; i < strArray.Length; i++)
            {
                string filepath = strArray[i];
                try
                {
                    bool isUse = validationFunc(filepath);
                    if (!isUse && File.GetCreationTime(filepath) < time)
                    {
                        File.Delete(filepath);
                        num++;
                    }
                }
                catch
                {
                }
            }
        }

        private string ResolvePath(string virtualPath)
        {
            string appRelativePath = HttpRuntime.AppDomainAppVirtualPath;
            if (appRelativePath != null && !appRelativePath.EndsWith("/"))
            {
                appRelativePath += "/";
            }

            return VirtualPathUtility.ToAbsolute(virtualPath, appRelativePath);
        }

        private string GetPhyiscalPath(string resolvedPath)
        {
            if (HostingEnvironment.IsHosted)
            {
                return HostingEnvironment.MapPath(resolvedPath);
            }
            else
            {
                return Path.Combine(Environment.CurrentDirectory, resolvedPath.Replace('/', '\\'));
            }
        }

        private bool PathExists(string path)
        {
            return File.Exists(path);
        }
    }
}
