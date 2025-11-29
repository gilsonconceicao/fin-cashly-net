using FinCashly.Domain.Security;
using Microsoft.AspNetCore.Authorization;

namespace FinCashly.API.Configurations.Attributes; 

public class PermissionAttribute : AuthorizeAttribute
{
    public PermissionAttribute(PermissionResource resource, PermissionType type)
    {
        Policy = $"{resource}.{type}";
    }
}