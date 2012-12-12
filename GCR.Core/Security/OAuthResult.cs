using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCR.Core.Security
{
    public class OAuthResult
    {
        public OAuthResult()
        {
        }

        public string EncryptedLoginData { get; set; }

        public bool IsValid { get; set; }

        public string ProviderDisplayName { get; set; }

        public string Provider { get; set; }

        public string ProviderUserId { get; set; }

        public string UserName { get; set; }

        public Exception Error { get; set; }
    }
}
