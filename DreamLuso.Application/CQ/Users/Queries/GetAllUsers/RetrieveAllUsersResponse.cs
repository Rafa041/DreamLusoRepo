using DreamLuso.Application.CQ.Users.Queries.Retrieve;
using DreamLuso.Domain.Model;

namespace DreamLuso.Application.CQ.Users.Queries.GetAllUsers;

public class RetrieveAllUsersResponse
{
    public IEnumerable<RetrieveUserResponse> Users { get; set; }
}