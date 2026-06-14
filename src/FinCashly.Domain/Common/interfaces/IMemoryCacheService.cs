namespace FinCashly.Domain.Common.interfaces
{
    public interface IMemoryCacheService
    {
        Task<T?> GetOrSetAsync<T>(
            string key, 
            Func<Task<T>> factory,
            TimeSpan expiration
        );

        void Remove(string key);
    }
}