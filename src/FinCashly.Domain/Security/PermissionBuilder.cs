namespace FinCashly.Domain.Security;

public static class PermissionBuilder
{
    public static string Build(PermissionResource resource, PermissionType type) => $"{resource}.{type}";
}