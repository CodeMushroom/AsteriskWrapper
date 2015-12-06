using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AsteriskWrapper.Exceptions
{
    public class AsteriskException : Exception
    {
        public int Code { get; set; }

        internal AsteriskException(int code, string message)
            : base(message)
        {
            Code = code;
        }
    }
}
