using DreamLuso.Domain.Model;

namespace DreamLuso.Security.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}
