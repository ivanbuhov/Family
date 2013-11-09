using Family.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Family.Services.Utils
{
    public class FamilyValidator : IFamilyValidator
    {
        public const int MinUsernameLength = 5;
        public const int MaxUsernameLength = 30;
        public const int MinPasswordLength = 5;
        public const int MaxPasswordLength = 30;
        public const int AuthCodeLength = 40;

        public void ValidateUsername(string username)
        {
            if (username == null || username.Length < MinUsernameLength || MaxUsernameLength < username.Length)
            {
                throw new FamilyValidationException(
                    string.Format("Username must be between {0} and {1} characters", MinUsernameLength, MaxUsernameLength));
            }
            foreach (char character in username)
            {
                if (!Char.IsLetterOrDigit(character))
                {
                    throw new FamilyValidationException("The username must contains only letters and digits.");
                }
            }
        }

        public void ValidatePassword(string password)
        {
            if (password == null || password.Length < MinPasswordLength || password.Length > MaxPasswordLength)
            {
                throw new FamilyValidationException(
                    string.Format("Password must be between {0} and {1} characters", MinPasswordLength, MaxPasswordLength));
            }
        }

        public void ValidateAuthCode(string authCode)
        {
            if (string.IsNullOrEmpty(authCode) || authCode.Length != AuthCodeLength)
            {
                throw new FamilyValidationException("Invalid username or password.");
            }
        }
    }
}