using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Application.Services;
using AtelieDaTransformacao.Domain.Interfaces;
using AtelieDaTransformacao.Infrastructure.Context;
using AtelieDaTransformacao.Infrastructure.Identity;
using AtelieDaTransformacao.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =====================================================================
// 1. BANCO DE DADOS
// =====================================================================
builder.Services.AddDbContext<AtelieDaTransformacaoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// =====================================================================
// 2. ASP.NET CORE IDENTITY
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

// =====================================================================
// 3. INJEÇÃO DE DEPENDÊNCIA (DI)
// =====================================================================
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IWhatsAppService, WhatsAppService>();

builder.Services.AddControllers();

// =====================================================================
// 4. SWAGGER DOCUMENTAÇÃO
// =====================================================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // 💡 Utiliza a configuração implícita padrão para evitar erros de compilação com namespaces ocultos

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// =====================================================================
// PIPELINE MIDDLEWARES
// =====================================================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// =====================================================================
// SEED DATA
// =====================================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var adminEmail = builder.Configuration["AdminSettings:Email"] ?? "admin@atelie.com";
    var adminPassword = builder.Configuration["AdminSettings:Password"] ?? "Admin@Atelie123";

    await SeedData.InitializeAsync(services, adminEmail, adminPassword);
}

app.Run();