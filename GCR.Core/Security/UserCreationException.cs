using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCR.Core.Security
{
    public class UserCreationException : Exception
    {
        public UserCreationException()
            : base()
        {
        }

        public UserCreationException(string message)
            : base(message)
        {
        }

        public UserCreationException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}
