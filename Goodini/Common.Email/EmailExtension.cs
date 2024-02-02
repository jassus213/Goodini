using System.ComponentModel.DataAnnotations;

namespace Common.Email;

public static class EmailExtension
{
    private static readonly EmailAddressAttribute _emailAddressAttribute = new EmailAddressAttribute();

    public static bool IsEmailValid(string email) => _emailAddressAttribute.IsValid(email);
}