using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ProverStore.Api.Data;
using ProverStore.Api.ViewModel;
using ProverStore.Business.Interface;
using ProverStore.Business.Model;
using ProverStore.Business.Notificacoes;
using ProverStore.Business.Services;
using ProverStore.Data.Context;
using ProverStore.Data.Repository;

namespace ProverStore.Api.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services) 
        {
            services.AddScoped<AppDbContext>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoService, ProdutoService>();
          //  services.AddScoped<IShoppingCartRepository, CarrinhoRepository>();
            services.AddScoped<IClienteRepository,ClienteRepository>();
            services.AddScoped<IClienteService, ClienteService>(); 
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<IFavoritoRepository, FavoritoRepository>();
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<JwtSettings>();
            services.AddSingleton<GoogleJsonWebSignature>();

            services.AddLogging();

            return services;
        }
    }
}
