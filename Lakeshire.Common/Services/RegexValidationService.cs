using System.Net.Mail;

namespace Lakeshire.Common.Services;

public sealed class RegexValidationService
{
    public bool TryValidateEmailAddress(string input, out MailAddress? mailAddress)
    {
        return MailAddress.TryCreate(input, out mailAddress);
    }
}