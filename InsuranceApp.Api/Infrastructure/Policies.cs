namespace InsuranceApp.Api.Infrastructure;

/// <summary>
/// Provides application-wide authorization policy names.
/// </summary>
public static class Policies
{
    /// <summary>
    /// Represents a policy that allows access to users in either
    /// the <see cref="UserRoles.Admin"/> or <see cref="UserRoles.Manager"/> roles.
    /// </summary>
    public const string AdminOrManager = "AdminOrManager";
}
