namespace DreamLuso.Security.Interfaces;

public interface IUserResolverService
{
    Guid GetUserId();
    string GetUserName();
    string GetUserEmail();
}