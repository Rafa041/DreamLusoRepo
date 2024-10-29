namespace DreamLuso.Application.Common.Responses;

public sealed record Error(string Code, string Description)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NotImplemented = new("NotImplemented", "Method not implemented");
    public static readonly Error EmptyDatabase = new("EmpytDatabase", "Not data found");
    public static readonly Error ExistingUser = new("ExistingUser", "There is a user with this email");
    public static readonly Error InvalidInput = new("InvalidInput", "The input provided is invalid");
    public static readonly Error UnauthorizedAccess = new("UnauthorizedAccess", "User is not authorized to perform this action");
    public static readonly Error OperationFailed = new("OperationFailed", "The operation failed to complete");
    public static readonly Error Timeout = new("Timeout", "The operation timed out");
    public static readonly Error ServiceUnavailable = new("ServiceUnavailable", "The service is currently unavailable");
    public static readonly Error DatabaseError = new("DatabaseError", "An error occurred while accessing the database");
    public static readonly Error InvalidCredentials = new("InvalidCredentials", "The provided credentials are invalid");
    public static readonly Error UserLockedOut = new("UserLockedOut", "The user account is locked out");
    public static readonly Error PasswordExpired = new("PasswordExpired", "The user password has expired");
    public static readonly Error ConcurrencyConflict = new("ConcurrencyConflict", "A concurrency conflict occurred");
    public static readonly Error ResourceConflict = new("ResourceConflict", "A resource conflict occurred");
    public static readonly Error InsufficientPermissions = new("InsufficientPermissions", "Insufficient permissions to perform this action");
    public static readonly Error InvalidToken = new("InvalidToken", "The provided token is invalid or expired");
    public static readonly Error NotFound = new("NotFound", "The requested resource was not found");
    public static readonly Error ExternalServiceError = new("ExternalServiceError", "An error occurred in an external service");
    public static readonly Error BadRequest = new("BadRequest", "The request was invalid or cannot be served");
    public static readonly Error Conflict = new("Conflict", "The request could not be completed due to a conflict");


    public static readonly Error ExistingProperty = new("ExistingProperty", "There is already a property with this title and address");
    public static readonly Error AccountNotFound = new("AccountNotFound", "Account Not Found");
    public static readonly Error InvalidPassword = new("InvalidPassword", "Invalid Password");
    public static readonly Error ExistingCategory = new("ExistingCategory", "This category already exists");
    public static readonly Error CategoryNotFound = new("CategoryNotFound", "Category Not Found");
    public static readonly Error CategoryNameAlreadyInUse = new("CategoryNameAlreadyInUse", "Category Name Already In Use");
    public static readonly Error UserNotFound = new("UserNotFound", "User Not Found");
    public static readonly Error ClientNotFound = new("ClientNotFound", "Client Not Found");
    public static readonly Error ClientsEmptyDatabase = new("ClientsEmptyDatabase", "Not data found");
    public static readonly Error CommentNotFound = new("CommentNotFound", "Comment Not Found");
    public static readonly Error ContractNotFound = new("ContractNotFound", "Contract Not Found");
    public static readonly Error ExistingAddress = new("ExistingAddress", "Existing Address");
    public static readonly Error RealStateAgentNotFound = new("RealStateAgentNotFound", "Real State Agent Not Found");
    public static readonly Error InvalidLanguage = new("InvalidLanguage", "The input provided is invalid");
    public static readonly Error InvalidAntiForgery = new("InvalidAntiForgery", "Invalid anti-forgery token");
    public static readonly Error InvalidDateOfBirth = new("InvalidDateOfBirth", "The provided date of birth is invalid.");
    public static readonly Error PropertyNotFound = new("PropertyNotFound", "Property Not Found");
    public static readonly Error NotificationNotFound = new("NotificationNotFound", "Notification Not Found");
    public static readonly Error InvalidNotificationData = new("InvalidNotificationData", "Invalid Notification Data");
}
