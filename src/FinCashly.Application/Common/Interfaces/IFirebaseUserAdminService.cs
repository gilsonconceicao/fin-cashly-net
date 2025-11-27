namespace FinCashly.Application.Common.Interfaces;

public interface IFirebaseUserAdminService
{
    Task SetRoleAsync(string uid, string role);
    Task<string?> GetRoleAsync(string uid);
}