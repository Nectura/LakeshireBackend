using Microsoft.Extensions.DependencyInjection;

namespace Lakeshire.Common.Validators.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddValidators<SyncValidatorType, AsyncValidatorType>(this IServiceCollection serviceCollection)
    {
        var syncValidatorType = typeof(SyncValidatorType);
        var asyncValidatorType = typeof(AsyncValidatorType);
        var validatorTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => p.IsClass && p.IsPublic && (syncValidatorType.IsAssignableFrom(p) || asyncValidatorType.IsAssignableFrom(p)))
            .ToList();
        foreach (var validatorType in validatorTypes)
            serviceCollection.AddScoped(validatorType);
        return serviceCollection;
    }
}