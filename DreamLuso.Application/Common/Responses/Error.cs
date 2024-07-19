namespace DreamLuso.Application.Common.Responses;

public sealed record Error(string Code, string Description)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NotImplemented = new("NotImplemented", "Method not implemented");
    public static readonly Error EmptyDatabase = new("EmpytDatabase", "Not data found");
    public static readonly Error ExistingUser = new ("ExistingUser", "There is a user with this email");
    public static readonly Error NotFoundUser = new("Not Found User", "Not found ");
}
