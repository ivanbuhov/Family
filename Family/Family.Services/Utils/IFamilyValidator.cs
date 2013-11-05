using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family.Services.Utils
{
    public interface IFamilyValidator
    {
        void ValidateUsername(string username);
        void ValidatePassword(string password);
        void ValidateAuthCode(string authCode);
    }
}
