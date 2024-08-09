using System.Security.Cryptography;
using System.Text;
using DreamLuso.Security.Interfaces;

namespace DreamLuso.Security.Services;

public class DataProtectionService : IDataProtectionService
{

    public record DataProtectionKeys(byte[] PasswordHash, byte[] PasswordSalt);

    public DataProtectionKeys Protect(string password)
    {
        using var hmac = new HMACSHA512();

        byte[] hashedPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        byte[] saltKey = hmac.Key;

        return new DataProtectionKeys(hashedPass, saltKey);
    }

    public byte[] GetComputedHash(string password, byte[] salt)
    {
        using var hmac = new HMACSHA512(salt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash;
    }
    public bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
    {
        var computedHash = GetComputedHash(password, storedSalt);

        // Compare the computed hash with the stored hash
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != storedHash[i])
            {
                return false;
            }
        }

        return true;
    }
}
