using Bitinvest.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Bitinvest.Infra.Repositories
{
    public static class RepositoriesConfig
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryCliente, RepositoryCliente>();
            services.AddScoped<IRepositoryContaReais, RepositoryContaReais>();
            services.AddScoped<IRepositoryContaCripto, RepositoryContaCripto>();
            return services;
        }
    }

}
