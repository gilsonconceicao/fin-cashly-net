namespace FinCashly.Domain.Common.interfaces
{
    public interface ICacheKeysService
    {
        string Categories(string userId);
        string Goals(string userId);
    }
}