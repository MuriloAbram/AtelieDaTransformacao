// =============================================================================
// AtelieDaTransformacao.API - Program.cs
// =============================================================================
// 📌 CONCEITO IMPORTANTE: Program.cs
// Este é o PONTO DE ENTRADA da aplicação API.
// Aqui configuramos todos os serviços (DI), middlewares e a pipeline HTTP.
//
// O que é configurado aqui:
// 1. Entity Framework Core (conexão com banco de dados)
// 2. ASP.NET Core Identity & Autenticação JWT (Token seguro stateless)
// 3. Dependency Injection (repositórios e serviços)
// 4. Swagger (configurado com suporte a envio de Token Bearer)
// 5. CORS (permissões de acesso cross-origin)
// =============================================================================

using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Application.Services;
using AtelieDaTransformacao.Domain.Interfaces;
using AtelieDaTransformacao.Infrastructure.Context;
using AtelieDaTransformacao.Infrastructure.Identity;
using AtelieDaTransformacao.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// =====================================================================
// 1. ENTITY FRAMEWORK CORE — Configuração do banco de dados
// =====================================================================
builder.Services.AddDbContext<AtelieDaTransformacaoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// =====================================================================
// 2. ASP.NET CORE IDENTITY & CONFIGURAÇÃO JWT — Autenticação e Autorização
// =====================================================================
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AtelieDaTransformacaoDbContext>()
.AddDefaultTokenProviders();

// 💡 CONFIGURAÇÃO DO JWT: Força a API a proteger os endpoints com Token Bearer (sem cookies)
var jwtSecret = builder.Configuration["JwtSettings:Secret"] ?? "ChaveMestraSuperSecretaDoAtelieDaTransformacao2026!";
var key = Encoding.ASCII.GetBytes(jwtSecret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

// =====================================================================
// 3. DEPENDENCY INJECTION — Registro de Repositórios e Serviços
// =====================================================================
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IWhatsAppService, WhatsAppService>();

// =====================================================================
// 4. CONTROLLERS
// =====================================================================
builder.Services.AddControllers();

// =====================================================================
// 5. SWAGGER — Documentação automática preparada para JWT
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

    // 💡 ADICIONADO: Adiciona o botão "Authorize" cadeado no Swagger para permitir testes com o Token JWT
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Autenticação baseada em Token JWT. Insira: Bearer {seu_token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

// 📌 IMPORTANTE: UseAuthentication processa o JWT antes do UseAuthorization aplicar as regras
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// =====================================================================
// SEED DATA — Popula o banco com dados iniciais do Administrador
// =====================================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var adminEmail = builder.Configuration["AdminSettings:Email"] ?? "admin@atelie.com";
    var adminPassword = builder.Configuration["AdminSettings:Password"] ?? "Admin@Atelie123";

    await SeedData.InitializeAsync(services, adminEmail, adminPassword);
}

app.Run();