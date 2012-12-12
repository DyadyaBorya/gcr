using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCR.Core.Entities;

namespace GCR.Core.Services
{
    public interface IUserService
    {
        bool Login(string username, string password, bool persist);

        void Logout();
    }
}
