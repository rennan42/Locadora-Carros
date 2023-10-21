using FluentValidation;
using LocadoraCarros.Application.MappingProfiles;
using LocadoraCarros.Application.Veiculos.Comandos.Criar;
using LocadoraCarros.Domain.Repositorios;
using LocadoraCarros.Infrastructure;
using LocadoraCarros.Infrastructure.Repositorios;
using MediatR.NotificationPublishers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LocadoraCarros.Application
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CriarVeiculoComandoHandler).GetTypeInfo().Assembly);
                cfg.NotificationPublisher = new ForeachAwaitPublisher();
            });

            services.AddAutoMapper(typeof(VeiculoMappingProfile));
            services.AddScoped<IVeiculoRepositorio, VeiculoRepositorio>();
            services.AddScoped<IVeiculoEventoRepositorio, VeiculoEventoRepositorio>();
            services
                .AddValidatorsFromAssemblies(
                new List<Assembly>
                {
                    typeof(CriarVeiculoComandoValidador).GetTypeInfo().Assembly
                }.AsEnumerable());
            services.AddDbContext<Contexto>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
