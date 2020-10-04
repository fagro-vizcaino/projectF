using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using LanguageExt;
using LanguageExt.Common;

namespace ProjectF.Data.Entities.Common.ValueObjects
{
    public class Email
    {
        public string Value { get; }

        protected Email() {}
        public Email(string value)
        {
            Value = value.ToLower();
        }

        public static Either<Error, Email> Of(string code) => IsValidEmail(code);

        static Either<Error, Email> IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Error.New("Email should not be empty");

            email = email.Trim().ToLower();

            if (email.Length > 200)
                return Error.New("Email is too long");

            try
            {
                MailAddress m = new MailAddress(email);
                return new Email(email);
            }
            catch (FormatException ex)
            {
                return Error.New($"Email is invalid \n {ex.Message}");
            }
        }

        public static implicit operator string(Email email)
        {
            return email.Value;
        }
    }
}