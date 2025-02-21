﻿using System.Reflection;
using UniversalCourt.Infrastructure.Extensions;
using UniversalCourt.Infrastructure.TenantSupport;
using UniversalCourt.Persistence.EF.DbMigrator;
using UniversalCourt.Persistence.EF.SQL;
using UniversalCourt.Persistence.EF.SQL.Extensions;
using Microsoft.Extensions.Configuration;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(configBuilder =>
    {
        // need to set base path to make it work with shared appsettings files
        configBuilder.SetBasePath(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location));
    })
    .ConfigureServices((hostContext, services) =>
    {
       // services.AddTenantSupport(hostContext.Configuration);
        services.AddATenantSupport(hostContext.Configuration);
        services.AddEntityFrameworkSqlServer<DashboardDbContext>();
        services.AddHostedService<DbMigratorHostedService>();
 
    })
    .Build();

await host.RunAsync();
