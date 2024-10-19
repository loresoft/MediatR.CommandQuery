namespace MediatR.CommandQuery.Services;

public static class ClaimNames
{
    public const string ObjectIdenttifier = "oid";

    public const string Subject = "sub";
    public const string NameClaim = "name";
    public const string EmailClaim = "email";
    public const string EmailsClaim = "emails";
    public const string ProviderClaim = "idp";
    public const string PreferredUserName = "preferred_username";

    public const string IdentityClaim = "http://schemas.microsoft.com/identity/claims/identityprovider";
    public const string IdentifierClaim = "http://schemas.microsoft.com/identity/claims/objectidentifier";

    public const string UserId = "oid";
    public const string EmployeeNumber = "emp";

    public const string RuleClaim = "rules";
}
