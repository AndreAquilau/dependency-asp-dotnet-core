using Microsoft.Data.SqlClient;
using DependencyStore.Repositories;
using DependencyStore.Repositories.Contracts;
using DependencyStore.Services;
using DependencyStore.Services.Contracts;
using DependencyStore;


namespace DependencyStore.Extensions;

public static class DependenciesExtension
{
    public static void AddSqlConnection(this IServiceCollection services, string connection) 
    {
        services.AddScoped(x => new SqlConnection(connection)); // Único por requisição, deixa o objeto em memória
    }
    
    public static void AddRepositories(this IServiceCollection services)
    {
        // builder.Services.AddTransient<CustomerRepository>();
        // builder.Services.AddTransient(new CustomerRepository());
        services.AddTransient<ICustomerRepository, CustomerRepository>(); // Cria uma nova instância a cada chamda
        services.AddTransient<IPromoCodeRepository, PromoCodeRepository>();
    }

    public static void AddServices(this IServiceCollection services) 
    {
        services.AddTransient<IDeliveryFeeService, DeliveryFeeService>();
    }

    public static void AddConfigurations(this IServiceCollection services)
    {
        services.AddSingleton<Configuration>(); // cria uma instância única até o encerramento do app
    }
}