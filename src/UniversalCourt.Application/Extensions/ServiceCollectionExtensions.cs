using UniversalCourt.Application.Repository.Base;
using UniversalCourt.Application.Repository.GeneralRepository.Projects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UniversalCourt.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddSingleton<ITenantProvider, TenantProvider>();
        services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>)); 
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
