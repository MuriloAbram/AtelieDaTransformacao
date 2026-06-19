using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AtelieDaTransformacao.Domain.Entities;

namespace AtelieDaTransformacao.Infrastructure.Context
{
    public class AtelieDaTransformacaoDbContext : IdentityDbContext //•	Declara a classe DbContext da aplicação que herda de IdentityDbContext para incluir tabelas/funcionalidades de Identity.
    {
        public AtelieDaTransformacaoDbContext(DbContextOptions<AtelieDaTransformacaoDbContext> options) : base(options) //•	Construtor que recebe opções do EF (conexão, provider, etc.) e as repassa para o construtor base (IdentityDbContext) para inicialização.
        {
        }

        public DbSet<Product> Products { get; set; } //•	Define um conjunto (DbSet) para a entidade Product; representa a tabela Products no banco.
        public DbSet<ProductCategory> ProductCategories { get; set; } //•	Define o DbSet para ProductCategory; representa a tabela ProductCategories.
        public DbSet<Customer> Customers { get; set; } //•	Define o DbSet para Customer; representa a tabela Customers.

        protected override void OnModelCreating(ModelBuilder modelBuilder) //•	Método override onde se configura o modelo EF (mapeamentos, constraints, relacionamentos).
        {
            base.OnModelCreating(modelBuilder);//•	Chama a implementação base para configurar entidades do Identity (usuários, roles, etc.).

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AtelieDaTransformacaoDbContext).Assembly);//•	Procura e aplica automaticamente todas as classes que implementam IEntityTypeConfiguration<T> no mesmo assembly (ex.: ProductConfiguration, ProductCategoryConfiguration), centralizando configurações de mapeamento.
        }
    }
}