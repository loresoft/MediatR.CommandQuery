namespace MediatR.CommandQuery.Audit
{
    public enum AuditOperation
    {
        None,
        Create,
        Read,
        Update,
        Delete,
        Login,
        Logout,
        Other
    }
}