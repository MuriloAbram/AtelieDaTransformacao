// =============================================================================
// AtelieDaTransformacao.API - Program.cs
// =============================================================================
// 📌 CONCEITO IMPORTANTE: Program.cs
// Este é o PONTO DE ENTRADA da aplicação API.
// Aqui configuramos todos os serviços (DI), middlewares e a pipeline HTTP.
//
// O que é configurado aqui:
// 1. Entity Framework Core (conexão com banco de dados)
// 2. ASP.NET Core Identity (autenticação e autorização)
// 3. Dependency Injection (repositórios e serviços)
// 4. Swagger (documentação da API)
// 5. CORS (permissões de acesso cross-origin)
// =============================================================================

using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Application.Services;
using AtelieDaTransformacao.Domain.Interfaces;
using AtelieDaTransformacao.Infrastructure.Context;
using AtelieDaTransformacao.Infrastructure.Identity;
using AtelieDaTransformacao.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Microsoft.AspNetCore.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// =====================================================================
// 1. ENTITY FRAMEWORK CORE — Configuração do banco de dados
// =====================================================================
// 📌 CONCEITO: AddDbContext registra o DbContext no container de DI.
// UseSqlServer configura o Entity Framework para usar o SQL Server.
// A connection string é lida do arquivo appsettings.json.
// =====================================================================
builder.Services.AddDbContext<AtelieDaTransformacaoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// =====================================================================
// 2. ASP.NET CORE IDENTITY — Autenticação e Autorização
// =====================================================================
// 📌 CONCEITO: Identity é o sistema de autenticação do ASP.NET Core.
// .AddRoles<IdentityRole>() ativa o suporte aos papéis (Admin, User, etc).
// sem essa linha, o método 'RoleExistsAsync' lança erro de compilação/execução.
// =====================================================================
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Configurações de senha (simplificadas para ensino)
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
})
.AddRoles<IdentityRole>() // 💡 ADICIONADO: Habilita o gerenciamento de Roles no sistema!
.AddEntityFrameworkStores<AtelieDaTransformacaoDbContext>()
.AddDefaultTokenProviders();

// Configuração de Cookie Authentication para a API
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = 403;
        return Task.CompletedTask;
    };
});

// =====================================================================
// 3. DEPENDENCY INJECTION — Registro de Repositórios e Serviços
// =====================================================================
// 📌 CONCEITO: Dependency Injection (DI)
// AddScoped registra um serviço com ciclo de vida "por requisição".
// Isso significa que uma nova instância é criada para cada requisição HTTP.
// =====================================================================
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IWhatsAppService, WhatsAppService>(); // Serviço do WhatsApp

// =====================================================================
// 4. CONTROLLERS
// =====================================================================
builder.Services.AddControllers();

// =====================================================================
// 5. SWAGGER — Documentação automática da API
// =====================================================================
// 📌 CONCEITO: Swagger gera automaticamente uma interface visual
// para testar os endpoints da API no navegador.
// Acesse: https://localhost:PORTA/swagger
// =====================================================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Atelie da Transformação API",
        Version = "v1",
        Description = "API REST do sistema Ateliê da Transformação — E-commerce integrado ao WhatsApp"
    });
});

// =====================================================================
// 6. CORS — Permite requisições de outras origens
// =====================================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// =====================================================================
// PIPELINE DE MIDDLEWARES
// =====================================================================
// 📌 CONCEITO: Middlewares são executados em sequência para cada requisição.
// A ordem importa! Cada middleware processa a requisição e passa adiante.
// =====================================================================

if (app.Environment.IsDevelopment())
{
    // Swagger só é habilitado em ambiente de desenvolvimento
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

// 📌 IMPORTANTE: UseAuthentication ANTES de UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// =====================================================================
// SEED DATA — Popula o banco com dados iniciais do Administrador
// =====================================================================
// 📌 CONCEITO: O seed é executado na inicialização da aplicação.
// Usamos o escopo criado para passar os parâmetros exigidos pelo SeedData do Ateliê.
// =====================================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var adminEmail = builder.Configuration["AdminSettings:Email"] ?? "admin@atelie.com";
    var adminPassword = builder.Configuration["AdminSettings:Password"] ?? "Admin@Atelie123";

    // Executa o método InitializeAsync do seu primeiro SeedData sem alterá-lo
    await SeedData.InitializeAsync(services, adminEmail, adminPassword);
}

app.Run();