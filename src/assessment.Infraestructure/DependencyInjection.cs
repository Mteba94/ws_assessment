using assessment.Application.Interfaces.ExternalWS;
using assessment.Application.Interfaces.Persistence;
using assessment.Application.Interfaces.Services;
using assessment.Infraestructure.Persistence.Context;
using assessment.Infraestructure.Persistence.ExternalWS;
using assessment.Infraestructure.Persistence.Repositories;
using assessment.Infraestructure.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace assessment.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastucture(this IServiceCollection services, ConfigurationManager configuration)
    {
        var assembly = typeof(ApplicationDbContext).Assembly.FullName;

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Connection"), x => x.MigrationsAssembly(assembly));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        var infraAsm = Assembly.GetExecutingAssembly();
        foreach (var impl in infraAsm.GetTypes()
                     .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository")))
        {
            var @interface = impl.GetInterfaces()
                .FirstOrDefault(i => i.Name == "I" + impl.Name);
            if (@interface != null)
                services.AddScoped(@interface, impl);
        }

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IOrderingQuery, OrderingQuery>();

        services.ConfigureHttpClientDefaults(builder =>
        {
            builder.ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(configuration["ExternalServices:ws_analiza_sentimiento"]!);

                var apiKey = configuration["ExternalServices:key"];

                if (!string.IsNullOrEmpty(apiKey))
                {
                    client.DefaultRequestHeaders.Add("ocp-apim-subscription-key", apiKey);
                }
            });
        });

        services.AddTransient<IAnalizaSentimientosAPI, AnalizaSentimientosAPI>();

        //services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        //services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}
