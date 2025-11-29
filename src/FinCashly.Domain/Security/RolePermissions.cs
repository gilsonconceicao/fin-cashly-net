namespace FinCashly.Domain.Security;

public static class RolePermissions
{
    public static IReadOnlyDictionary<string, string[]> Map = new Dictionary<string, string[]>
    {
        [Roles.Admin] = new[]
        {
            // User
            PermissionBuilder.Build(PermissionResource.User, PermissionType.Read),
            PermissionBuilder.Build(PermissionResource.User, PermissionType.Create),
            PermissionBuilder.Build(PermissionResource.User, PermissionType.Update),
            PermissionBuilder.Build(PermissionResource.User, PermissionType.Delete), 
            
            // Account
            PermissionBuilder.Build(PermissionResource.Account, PermissionType.Read),
            PermissionBuilder.Build(PermissionResource.Account, PermissionType.Create),
            PermissionBuilder.Build(PermissionResource.Account, PermissionType.Update),
            PermissionBuilder.Build(PermissionResource.Account, PermissionType.Delete), 
            
             // Transaction
            PermissionBuilder.Build(PermissionResource.Transaction, PermissionType.Read),
            PermissionBuilder.Build(PermissionResource.Transaction, PermissionType.Create),
            PermissionBuilder.Build(PermissionResource.Transaction, PermissionType.Update),
            PermissionBuilder.Build(PermissionResource.Transaction, PermissionType.Delete),

            // Category
            PermissionBuilder.Build(PermissionResource.Category, PermissionType.Read),
            PermissionBuilder.Build(PermissionResource.Category, PermissionType.Create),
            PermissionBuilder.Build(PermissionResource.Category, PermissionType.Update),
            PermissionBuilder.Build(PermissionResource.Category, PermissionType.Delete), 

            // Goals
            PermissionBuilder.Build(PermissionResource.Goals, PermissionType.Read),
            PermissionBuilder.Build(PermissionResource.Goals, PermissionType.Create),
            PermissionBuilder.Build(PermissionResource.Goals, PermissionType.Update),
            PermissionBuilder.Build(PermissionResource.Goals, PermissionType.Delete)

        },

        [Roles.ReadOnly] = new[]
        {
            // User
            PermissionBuilder.Build(PermissionResource.User, PermissionType.Read),
            PermissionBuilder.Build(PermissionResource.User, PermissionType.ReadOnlyTESTE),
            
            // Account
            PermissionBuilder.Build(PermissionResource.Account, PermissionType.Read),
            
             // Transaction
            PermissionBuilder.Build(PermissionResource.Transaction, PermissionType.Read),

            // Category
            PermissionBuilder.Build(PermissionResource.Category, PermissionType.Read),

            // Goals
            PermissionBuilder.Build(PermissionResource.Goals, PermissionType.Read)
        }
    };
}
