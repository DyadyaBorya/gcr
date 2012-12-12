using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCR.Core.Security
{
    public class OAuthProvider
    {
        public OAuthProvider()
        {
        }

        public string ProviderName { get; set; }

        public string ProviderDisplayName { get; set; }

        public string ProviderUserId { get; set; }
    }
}
