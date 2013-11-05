using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Family.Services.Exceptions
{
    public class FamilyException : Exception
    {
        public FamilyException(string message)
            : this(message, null) { }
        public FamilyException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}