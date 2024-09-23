using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace PGP.Helpers;

public static class StringHelper
{
    public static string EncryptPassword(this string password)
    {
        var salt = "TFSK3YS@LT&KEY"u8.ToArray();

        var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

        return hash;
    }
    
    public static bool IsStrongPassword(this string password)
    {
        var regexPassword = new Regex("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@$!.;:></|,%*#?()=+{}&_-]).{6,20}$");

        return !regexPassword.IsMatch(password);
    }
    
    public static string SomenteNumeros(this string? value)
    {
        string numeros = string.Empty;
        if (string.IsNullOrEmpty(value))
            return numeros;

        return Regex.Match(value.SemMascara(), "\\d+").Value;
    }

    private static string SemMascara(this string? value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        return value.Replace("(","").Replace(")", "").Replace("_", "").Replace(" ", "").Replace(".", "").Replace("-", "").Replace("/", "");
    }
}