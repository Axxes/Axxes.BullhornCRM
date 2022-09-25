using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Axxes.BullhornCRM.Attributes;
using Axxes.BullhornCRM.DelegatingHandlers;
using Axxes.BullhornCRM.Models;
using Axxes.BullhornCRM.Utility;
using Axxes.BullhornCRM.Utility.Fetchers;
using Axxes.BullhornCRM.Utility.Models;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Axxes.BullhornCRM.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBullhornCRM(this IServiceCollection services, Action<BullhornAuthCredentials> options, params Assembly[] assemblies)
    {
        var auth = new BullhornAuthCredentials();
        options(auth);
        
        var settings = new RefitSettings(new NewtonsoftJsonContentSerializer());
        
        const string baseEntityUri = Settings.BaseUri + "entity/";
        const string baseQueryUri = Settings.BaseUri + "query/";
        
        services.AddSingleton(auth);
        
        var models = typeof(IBullhorn<>)
            .Assembly
            .GetExportedTypes()
            .Concat(assemblies.SelectMany(x => x.GetExportedTypes()))
            .Where(x => !x.IsAbstract && !x.IsInterface)
            .Where(x => typeof(IBullhornEntity).IsAssignableFrom(x))
            .ToArray();

        RegisterBullhornApis(baseEntityUri, services, settings, models);
        RegisterBullhornHistoryApis(baseQueryUri, services, settings, models);

        services.AddTransient<TokenProvider>();
        services.AddSingleton<BullhornTokenHandler>();
        services.AddTransient(typeof(FieldsHandler<>));

        services.AddHttpClient<CodeFetcher>();
        services.AddHttpClient<AuthorizationCodeFetcher>();
        services.AddHttpClient<RestTokenFetcher>();
        
        services.AddTransient<CodeFetcher>();
        services.AddTransient<AuthorizationCodeFetcher>();
        services.AddTransient<RestTokenFetcher>();
        
        return services;
    }

    private static void RegisterBullhornApis(string baseUri, IServiceCollection services, RefitSettings settings, IEnumerable<Type> models)
    {
        var bullhornType = typeof(IBullhorn<>);
        var fieldsHandlerType = typeof(FieldsHandler<>);

        foreach (var model in models)
        {
            var genericBullhornTypeForModel = bullhornType.MakeGenericType(model);
            var genericFieldsHandlerType = fieldsHandlerType.MakeGenericType(model);
            var entityName = model.GetCustomAttribute<EntityNameAttribute>(true);
            
            services
                .AddRefitClient(genericBullhornTypeForModel, settings)
                .ConfigureHttpClient(x =>
                {
                    x.BaseAddress = new Uri(baseUri + entityName.Name);
                    x.Timeout = TimeSpan.FromMinutes(10);
                })
                .ConfigurePrimaryHttpMessageHandler(sp => sp.GetRequiredService<BullhornTokenHandler>())
                .AddHttpMessageHandler(sp => (DelegatingHandler) sp.GetRequiredService(genericFieldsHandlerType));
        }
    }
    
    private static void RegisterBullhornHistoryApis(string baseUri, IServiceCollection services, RefitSettings settings, IEnumerable<Type> models)
    {
        var bullhornType = typeof(IBullhornEditHistory<>);

        foreach (var model in models.Where(x => typeof(IBullhornEntityWithHistory).IsAssignableFrom(x)))
        {
            var genericBullhornTypeForModel = bullhornType.MakeGenericType(model);
            var entityName = model.GetCustomAttribute<EntityHistoryNameAttribute>(true);
            
            services
                .AddRefitClient(genericBullhornTypeForModel, settings)
                .ConfigureHttpClient(x =>
                {
                    x.BaseAddress = new Uri(baseUri + entityName.Name);
                    x.Timeout = TimeSpan.FromMinutes(10);
                })
                .ConfigurePrimaryHttpMessageHandler(sp => sp.GetRequiredService<BullhornTokenHandler>());
        }
    }
}