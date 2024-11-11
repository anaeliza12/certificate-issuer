namespace Api.Certification.AppDomain.Interfaces
{
    public interface IRedisService
    {
        Task<bool> InsertCacheAsync(string key, string cache);
        Task<string> GetCacheAsync(string key);
    }
}
