using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AtelieDaTransformacao.Infrastructure.Context;
using AtelieDaTransformacao.Domain.Interfaces;
using AtelieDaTransformacao.Infrastructure.Repositories;
using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Application.Services;

namespace AtelieDaTransformacao.UI;

/// <summary>
/// Configuração principal de inicialização da aplicação, registro de dependências e pipelines HTTP.
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configuração do Banco de Dados SQL Server via EF Core
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new System.InvalidOperationException("Connection string 'DefaultConnection' not found.");

        builder.Services.AddDbContext<AtelieDaTransformacaoDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Configuração do ASP.NET Core Identity para Autenticação do Administrador
        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        })
        .AddEntityFrameworkStores<AtelieDaTransformacaoDbContext>()
        .AddDefaultTokenProviders();

        // Configurações de cookies de Autenticação/Redirecionamento de páginas restritas
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";
        });

        // Registro de Dependências por Escopo (Injeção de Dependência Manual)
        // Infraestrutura -> Repositórios
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

        // Aplicação -> Serviços de Negócio
        builder.Services.AddScoped<IWhatsAppService, WhatsAppService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();

        // Adiciona suporte para Controllers com Views (Razor Engine)
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Pipeline de requisições HTTP (Middleware)
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles(); // Ativa suporte para CSS, JavaScript e imagens na pasta wwwroot

        app.UseRouting();

        app.UseAuthentication(); // Middleware de validação de identidade (Quem é o utilizador)
        app.UseAuthorization();  // Middleware de validação de permissões (O que ele pode fazer)

        // Configuração de rotas padrão do MVC
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}