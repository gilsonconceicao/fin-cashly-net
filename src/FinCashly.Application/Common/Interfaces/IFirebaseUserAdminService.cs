namespace FinCashly.Application.Common.Interfaces;

public interface IFirebaseUserAdminService
{
    Task SetRoleAsync(string uid, string rule);
    Task<string?> GetRoleAsync(string uid);
}