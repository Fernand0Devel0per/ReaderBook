namespace ReaderBook.Core.Data.Caching.Interface;

public interface IChachingService
{
    Task SetAsync(string key, string value);
    Task<string> GetAsync(string key);
    Task RemoveAsync(string key);
}
