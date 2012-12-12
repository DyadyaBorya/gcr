using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCR.Core.Security
{
    public interface ISecurityProvider
    {
        bool Login(string username, string password, bool persist);

        bool Login(string username, string password);

        void Logout();
    }
}
