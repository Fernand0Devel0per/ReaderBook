using ReaderBook.Core.Data.Caching;

namespace ReaderBook.Core.Helpers.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddStackExchangeRedisCacheFromConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var redisSettings = new RedisSettings();
        configuration.GetSection("Redis").Bind(redisSettings);

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisSettings.Configuration;
            options.InstanceName = redisSettings.InstanceName;
        });
    }
}
