using FluentValidation;
using LocadoraCarros.Application.MappingProfiles;
using LocadoraCarros.Application.Veiculos.Comandos.Criar;
using LocadoraCarros.Domain.Repositorios;
using LocadoraCarros.Infrastructure;
using LocadoraCarros.Infrastructure.Repositorios;
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
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CriarVeiculoComandoHandler).GetTypeInfo().Assembly));
            services.AddAutoMapper(typeof(VeiculoMappingProfile));
            services.AddScoped<IVeiculoRepositorio, VeiculoRepositorio>();
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
