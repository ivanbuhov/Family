using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Family.Services.Exceptions
{
    public class FamilyValidationException : FamilyException
    {
        public FamilyValidationException(string message)
            : this(message, null) { }
        public FamilyValidationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}