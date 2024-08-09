using static DreamLuso.Security.Services.DataProtectionService;

namespace DreamLuso.Security.Interfaces;

public interface IDataProtectionService
{
    DataProtectionKeys Protect(string password);
    byte[] GetComputedHash(string password, byte[] salt);
    bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt);

}
