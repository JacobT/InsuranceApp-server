using System.Reflection;

namespace InsuranceApp.Api.Infrastructure;

/// <summary>
/// Provides application-wide constants and utilities for user roles.
/// </summary>
public static class UserRoles
{
    /// <summary>
    /// Represents the administrator role, typically with full permissions.
    /// </summary>
    public const string Admin = "admin";

    /// <summary>
    /// Represents the manager role, with elevated but limited permissions compared to <see cref="Admin"/>.
    /// </summary>
    public const string Manager = "manager";

    /// <summary>
    /// Represents the default application user role, with basic permissions.
    /// </summary>
    public const string User = "user";

    /// <summary>
    /// Gets all roles defined in this class.
    /// </summary>
    public static string[] AllRoles { get; } = GetAllRoles();

    /// <summary>
    /// Uses reflection to retrieve all public constant string fields defined in this class.
    /// </summary>
    /// <remarks>
    /// This method ensures <see cref="AllRoles"/> is always up to date
    /// if new roles are added as constants in this class.
    /// </remarks>
    /// <returns>
    /// An array of all role names defined as public constants in <see cref="UserRoles"/>.
    /// </returns>
    private static string[] GetAllRoles()
    {
        FieldInfo[] fields = typeof(UserRoles)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(field =>
                field.FieldType == typeof(string)
                && field.IsLiteral
                && !field.IsInitOnly)
            .ToArray();

        string[] roles = fields.Select(field => field.GetRawConstantValue())
            .OfType<string>()
            .ToArray();

        return roles;
    }
}
