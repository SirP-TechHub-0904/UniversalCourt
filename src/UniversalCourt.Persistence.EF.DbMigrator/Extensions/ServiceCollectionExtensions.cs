﻿using UniversalCourt.Infrastructure.TenantSupport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UniversalCourt.Persistence.EF.DbMigrator;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddATenantSupport(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ITenantProvider, TenantProvider>();
       services.Configure<TenantConfigurationOptions>(configuration.GetSection(TenantConfigurationOptions.ConfigKey));

        return services;
    }
}
